using Microsoft.AspNetCore.Mvc;


namespace Netcorewebapi.Controllers
{
    [Route("api/[controller]")]
    
    public class ValueController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new[] {"Value1", "Value2", "Value3"});
        }
    }
}
