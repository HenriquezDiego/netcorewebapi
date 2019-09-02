using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace NetcorewebApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class CoreController<TEntity,TParameters> : ControllerBase 
    {
        protected readonly IMapper Mapper;
        protected readonly ILogger<TEntity> Logger;
        protected CoreController(
            IMapper mapper,
            ILogger<TEntity> logger)
        {
            Mapper = mapper;
            Logger = logger;
        }

        [HttpGet]
        public abstract IActionResult GetAll(TParameters parameters);

        [HttpGet("{id}")]
        public abstract IActionResult Get(int id);

        [HttpGet("Async")]
        public abstract Task<IActionResult> GetAllAsync(TParameters parameters);

        [HttpGet("Async/{id}")]
        public abstract IActionResult GetAsync(int id);

        [HttpPost]
        public abstract IActionResult Post(TEntity model);

        [HttpPut("{id}")]
        public abstract IActionResult Put(int id,TEntity model);

        [HttpDelete("{id}")]
        public abstract IActionResult Delete(int id);

    }
}

