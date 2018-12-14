using Microsoft.AspNetCore.Mvc;


namespace netcorewebapi.Controllers
{
    [Route("api/[controller]")]
    
    public class ValueController : ControllerBase
    {
        public ValueController()
        {

        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new string [] {"Value1", "Value2", "Value3"});
        }

    }
}
