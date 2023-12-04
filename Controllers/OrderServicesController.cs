using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UtilizerAPI.Classes;
using UtilizerAPI.Models;

namespace UtilizerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderServicesController : ControllerBase
    {
        private readonly FinalDbContext _context;

        public OrderServicesController(FinalDbContext context)
        {
            _context = context;
        }

        [HttpPut("utilization/{id}")]
        public async Task<ActionResult<IEnumerable<ActionResult>>> PutUtilizationOrderService(int id)
        {
            try
            {
                var orderService = _context.OrderServices
                .Include(x => x.UtilizerNavigation)
                .Include(x => x.Result)
                .FirstOrDefault(x => x.Id == id);

                if (orderService.UtilizerNavigation.IsBusy)
                {
                    return NotFound("Утилизатор занят");
                }

                orderService.Status = 2;
                orderService.StartTime = DateTime.Now;
                orderService.PercentProgress = 0;

                if (orderService.Result != null)
                {
                    orderService.Result.Result1 = 0;
                }

                orderService.UtilizerNavigation.IsBusy = true;

                _context.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("progress/{id}")]
        public async Task<ActionResult<IEnumerable<ActionResult>>> PutProgressOrderService(int id)
        {
            var orderService = _context.OrderServices
                .Include(x => x.UtilizerNavigation)
                .Include(x => x.ServiceNavigation)
                .Include(x => x.Result)
                .FirstOrDefault(x => x.Id == id);

            await Task.Delay(1500);

            orderService.PercentProgress += 5;

            if (orderService.PercentProgress >= 100)
            {
                orderService.PercentProgress = 100;

                if (orderService.Result == null)
                {
                    orderService.Result = new Result()
                    {
                        Result1 = new Random().Next(10000) / 100
                    };
                }
                else
                {
                    orderService.Result.Result1 = new Random().Next(10000) / 100;
                }
                
                orderService.ExecutionTime = 30;

                var deviation = orderService.ServiceNavigation.AverageResult / orderService.Result.Result1;

                if (deviation >= 5 || (double)deviation <= 0.5)
                {
                    orderService.Status = 5;
                }
                else
                {
                    orderService.Status = 3;
                }

                orderService.UtilizerNavigation.IsBusy = false;
            }

            _context.SaveChanges(); 

            return Ok();
        }

        [HttpPut("approve/{id}")]
        public async Task<ActionResult<IEnumerable<ActionResult>>> PutApproveOrderService(int id)
        {
            var orderService = _context.OrderServices
                .FirstOrDefault(x => x.Id == id);

            orderService.Status = 4;

            _context.SaveChanges();

            return Ok();
        }

        private bool OrderServiceExists(int id)
        {
            return (_context.OrderServices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
