using Microsoft.AspNetCore.Mvc;


namespace Netcorewebapi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValueController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("200");
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        [HttpGet("/")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Swagger()
        {
            return Redirect("/swagger");
        }

    }
}
