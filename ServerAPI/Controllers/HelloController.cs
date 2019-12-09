using Microsoft.AspNetCore.Mvc;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("hello")]
    public class HelloController : ControllerBase
    {


        [HttpGet]
        public ActionResult<string> Get()
        {


            return Ok("hello");
        }


    }
}
