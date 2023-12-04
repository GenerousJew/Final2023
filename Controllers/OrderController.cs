using FinalAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[action]")]
    public class OrderController : Controller
    {
        private readonly FinalDbContext context;
        public OrderController(FinalDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult PostOrder([FromBody] Order newOrder)
        {
            context.Orders.Add(newOrder);
            context.SaveChanges();

            return Ok();
        }



        [HttpGet]
        public IActionResult GetOrderWithUser()
        {
            return Ok(context.Orders.Include(x => x.ClientNavigation).AsNoTracking());
        }

        [HttpGet]
        public IActionResult GetOrderService() {
            return Ok(context.OrderServices.Include(x => x.Order).AsNoTracking().ToList());
        }

        [HttpPost]
        public IActionResult PostOrderServices(int orderId, List<int> servicesCode)
        {
            for (int i = 0; i < servicesCode.Count; i++)
            {
                context.OrderServices.Add(new OrderService()
                {
                    Order = orderId,
                    Service = servicesCode[i]
                });
            }

            context.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public IActionResult GetOrder()
        {
            return Ok();
        }

    }
}
