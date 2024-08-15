using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Example07.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase 
    {
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello trananhdung@gmail.com");
        }
    }
}