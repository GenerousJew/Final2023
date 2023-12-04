using FinalAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[action]")]
    public class StaffController : Controller
    {
        private readonly FinalDbContext context;
        public StaffController(FinalDbContext context)
        {
            this.context = context;
        }

        [HttpGet("login={login}&password={password}&ip={ip}")]
        public IActionResult GetAutorization(string login, string password, string ip)
        {
            var item = context.Staff.FirstOrDefault(x => x.Login == login);

            if(item != null && item.Password == password)
            {
                context.LoginHistories.Add(new LoginHistory()
                {
                    Login = login,
                    Ip = ip,
                    Success = true,
                    Date = DateTime.Now
                });

                context.SaveChanges();

                return Ok(item);
            }
            else
            {
                context.LoginHistories.Add(new LoginHistory()
                {
                    Login = login,
                    Ip = ip,
                    Success = false,
                    Date = DateTime.Now
                });

                context.SaveChanges();

                return NotFound("Пользователь с такими данными не найден. Проверье логин или пароль.");
            }
        }

        public class CaseInfo
        {
            public List<Client> Clients { get; set; }
            public List<Company> Companies { get; set; }
            public List<Service> Services { get; set; }
            public int NextId { get; set; }
            public int NextNumber { get; set; }
        }

        [HttpGet]
        public IActionResult GetCaseInfo()
        {
            var caseInfo = new CaseInfo()
            {
                Clients = context.Clients.AsNoTracking().ToList(),
                Companies = context.Companies.AsNoTracking().ToList(),
                Services = context.Services.AsNoTracking().ToList(),
                NextId = context.Orders.AsNoTracking().ToList().Count() + 1,
                NextNumber = context.Orders.AsNoTracking().Where(x => x.Number != null).Select(x => (int)x.Number).OrderBy(x => x).LastOrDefault() + 1
            };

            return Ok(caseInfo);
        }

        [HttpGet]
        public IActionResult GetCompanies()
        {
            return Ok(context.Companies.AsNoTracking().ToList());
        }
    }
}
