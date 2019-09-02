using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Netcorewebapi.Common.Helpers;
using Netcorewebapi.DataAccess.Entities;
namespace NetcorewebApi.Api.Controllers
{
    public class TestController : CoreController<Product, ProductParameters>
    {
        public TestController(IMapper mapper, ILogger<Product> logger) : base(mapper, logger)
        {
        }

        public override IActionResult Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public override IActionResult Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public override IActionResult GetAll([FromQuery]ProductParameters parameters)
        {
            throw new System.NotImplementedException();
        }

        public override Task<IActionResult> GetAllAsync([FromQuery]ProductParameters parameters)
        {
            throw new System.NotImplementedException();
        }

        public override IActionResult GetAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public override IActionResult Post(Product model)
        {
            throw new System.NotImplementedException();
        }

        public override IActionResult Put(int id, Product model)
        {
            throw new System.NotImplementedException();
        }
    }
}
