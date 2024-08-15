using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResourcesController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IActionResult GetResources()
        {
            return Ok($"Protected resources, username: {User.Identity!.Name}");
        }
    }
}
