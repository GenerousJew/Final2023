using API.Classes;
using FinalAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static API.Controllers.OrderController;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[action]")]
    public class ResultController : Controller
    {
        private readonly FinalDbContext context;
        public ResultController(FinalDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetNews()
        {
            return Ok(context.News);
        }

        [HttpGet]
        public IActionResult GetServices()
        {
            return Ok(context.Services);
        }

        [HttpGet]
        public IActionResult GetResults(int serviceCode)
        {
            Service service = context.Services.Include(x => x.OrderServices)
                .ThenInclude(x => x.Result)
                .Include(x => x.OrderServices)
                .ThenInclude(x => x.OrderNavigation)
                .Single(x => x.Code == serviceCode);

            DeviationRecounter dr = new DeviationRecounter();

            List<decimal> results = service.OrderServices.Select(x => x.Result.Result1).ToList();

            if(results.Count == 0)
            {
                return Ok("Выполненные услуги не найдены");
            }

            List<decimal> mathResult = dr.Mathfun(results);

            decimal x = mathResult[0];
            decimal s = mathResult[1];

            Dictionary<DateTime, decimal> serviceResults = new Dictionary<DateTime, decimal>();

            service.OrderServices.ToList().ForEach(x => {
                var dt = x.OrderNavigation.CreateDate;
                while (serviceResults.ContainsKey(dt))
                {
                    dt = dt.AddSeconds(1);
                }
                serviceResults.Add(dt, x.Result.Result1);
            });

            var otvet = new Classes.JsonResult()
            {
                P1S = Decimal.Round(x + s, 2),
                P2S = Decimal.Round(x + 2 * s, 2),
                P3S = Decimal.Round(x + 3 * s, 2),
                M1S = Decimal.Round(x - s, 2),
                M2S = Decimal.Round(x - 2 * s, 2),
                M3S = Decimal.Round(x - 3 * s, 2),
                X = Decimal.Round(x, 2),
                S = Decimal.Round(s, 2),
                Coef = Decimal.Round(s / x * 100, 2),
                ResultDict = serviceResults
            };

            return Ok(otvet);
        }
    }
}
