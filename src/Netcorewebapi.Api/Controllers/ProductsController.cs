using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Netcorewebapi.Api.Helpers;
using Netcorewebapi.Api.ViewModels;
using Netcorewebapi.Common;
using Netcorewebapi.Common.Helpers;
using Netcorewebapi.DataAccess.Entities;
using NetcorewebApi.DataAccess.Persistence.Repositories;
using System.Collections.Generic;

namespace Netcorewebapi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUrlHelper _urlHelper;

        public ProductsController(IProductRepository repository, 
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
            var products = _repository.GetProductspaged(resourceParameters);
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
            var product =_repository.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public  IActionResult Post([FromBody]Product product)
        {
            _repository.Add(product);
            
            if (_repository.Save())
            {
                //return Created($"/api/product/{product.Id}", product);
                return CreatedAtAction("GetProduct", new {id = product?.Id}, product);
            }
            return BadRequest("Failed to save new Order");

        }
    }
}
