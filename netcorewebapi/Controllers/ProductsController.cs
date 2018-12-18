using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Netcorewebapi.DataAccess.Core;
using Netcorewebapi.DataAccess.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Netcorewebapi.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public ProductsController(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/Products
        [HttpGet]
        public  ActionResult<IEnumerable<Product>> GetProducts()
        {
            var products = _repository.GetAllProducts();
            if (products.Any())
            {
                return Ok(_mapper.Map<IEnumerable<Product>,IEnumerable<ProductViewModel>>(products));
            }

            return BadRequest();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product =_repository.GetProductsById(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        public async System.Threading.Tasks.Task<IActionResult> PostAsync([FromBody]Product product)
        {
            var flag = await _repository.AddEntityAsync(product);
            if (flag)
            {
                //return Created($"/api/product/{product.Id}", product);
                return CreatedAtAction("GetProduct", new {id = product.Id}, product);
            }
            return BadRequest("Failed to save new Order");

        }

        private bool ProductExists(int id)
        {
            return _repository.GetAllProducts().Any(e => e.Id == id);
        }
    }
}
