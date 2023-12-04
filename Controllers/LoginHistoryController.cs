using FinalAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[action]")]
    public class LoginHistoryController : Controller
    {
        private readonly FinalDbContext context;
        public LoginHistoryController(FinalDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetHistory()
        {
            return Ok(context.LoginHistories);
        }
    }
}
