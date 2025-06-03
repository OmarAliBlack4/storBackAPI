using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectAPI.PresentationLayer.Controllers.TestController
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {


        [HttpGet("publich")]
        public IActionResult PublichEndpoint()
        {
            return Ok("This is a publich endpoint");
        }


        [Authorize] 
        [HttpGet("protected")]
        public IActionResult ProtectedEndpoint()
        {
            return Ok("This is a protected endpoint");
        }



        [Authorize(Roles = "Admin")] 
        [HttpGet("admin-only")]
        public IActionResult AdminOnlyEndpoint()
        {
            return Ok("Only admins can see this");
        }
    }
}
