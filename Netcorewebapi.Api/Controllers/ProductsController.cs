using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Netcorewebapi.Common;
using Netcorewebapi.DataAccess.Core;
using Netcorewebapi.DataAccess.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using Netcorewebapi.Api.ViewModels;
using Netcorewebapi.Common.Helpers;

namespace Netcorewebapi.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUrlHelper _urlHelper;

        public ProductsController(IRepository repository, 
            IMapper mapper,
            IUrlHelper iUrlHelper)
        {
            _repository = repository;
            _mapper = mapper;
            _urlHelper = iUrlHelper;
        }

        // GET: api/Products
        [HttpGet(Name = "GetProducts")]
        public  ActionResult<IEnumerable<Product>> GetProducts([FromQuery]ProductParameters  resourceParameters)
        {
            var products = _repository.GetProductsPage(resourceParameters);
            var cResourceUri = new CreateResourceUri(_urlHelper); 
            

            var previousPageLink = products.HasPrevious ?
                cResourceUri.CreateProductResourceUri(resourceParameters,
                    ResourceUriType.PreviousPage,"GetProducts") : null;

            var nextPageLink = products.HasNext ? 
                cResourceUri.CreateProductResourceUri(resourceParameters,
                    ResourceUriType.NextPage,"GetProducts") : null;

            var paginationMetadata = new
            {
                totalCount = products.TotalCount,
                pageSize = products.PageSize,
                currentPage = products.CurrentPage,
                totalPages = products.TotalPage,
                previousPageLink,
                nextPageLink
            };

            Response.Headers.Add("X-Pagination",
                Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));
            return Ok( _mapper.Map<IList<Product>, IList<ProductViewModel>>(products));

            
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
