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
