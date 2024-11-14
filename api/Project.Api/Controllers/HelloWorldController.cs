using Microsoft.AspNetCore.Mvc;

namespace Project.Api.Controllers
{
    public class HelloWorldController : BaseApiController
    {
        [HttpGet]
        public IActionResult Index(){
            return Ok("Hello World!");
        }
    }
}