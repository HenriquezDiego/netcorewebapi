using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NetcorewebApi.Api.Controllers
{
    [ApiController]
    public class CoreController<TEntity,TParameters> : ControllerBase
    {
        
        [HttpGet]
        public virtual IActionResult Get(TParameters parameters)
        {
            return null;
        }

        [HttpGet("{id}")]
        public virtual IActionResult Get(int id) 
        {
            return null;
        }

        [HttpGet(template:"/GetAsync")]
        public virtual Task<IActionResult> GetAsync(TParameters parameters)
        {
            return null;
        }

        [HttpGet("/GetAsync/{id}")]
        public virtual Task<IActionResult> GetAsync(int id) 
        {
            return null;
        }

        [HttpPost]
        public virtual IActionResult Post(TEntity model)
        {
            return null;
        }

        [HttpPut("{id}")]
        public virtual IActionResult Put(int id, TEntity model)
        {
            return null;
        }

        [HttpDelete("{id}")]
        public virtual IActionResult Delete(int id)
        {
            
            return null;
            
        }

    }
}
