using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookShelfHaven5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // GET: api/Home
        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        // GET: api/Home/SuccessMessage
        [HttpGet("SuccessMessage")]
        public IActionResult SuccessMessage()
        {
            return Ok(new { Message = "Successfully added" });
        }

        // GET: api/Home/Privacy
        [HttpGet("Privacy")]
        public IActionResult Privacy()
        {
            return Ok();
        }

        // GET: api/Home/Error
        [HttpGet("Error")]
        public IActionResult Error()
        {
            return StatusCode(500, new { Error = "An error occurred" });
        }
    }
}
