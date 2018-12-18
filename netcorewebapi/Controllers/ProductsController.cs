using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Netcorewebapi.DataAccess.Core;
using Netcorewebapi.DataAccess.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace Netcorewebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        private bool ProductExists(int id)
        {
            if (_repository.GetAllProducts().Any(e => e.Id == id)) return true;
            return false;
        }
    }
}
