using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Netcorewebapi.Common.Helpers;
using Netcorewebapi.DataAccess.Entities;
using NetcorewebApi.DataAccess.Persistence.Repositories;

namespace NetcorewebApi.Api.Controllers
{
    public class TestController : CoreController<Product,ProductParameters>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUrlHelper _urlHelper;

        public TestController(IProductRepository repository, 
            IMapper mapper,
            IUrlHelper iUrlHelper)
        {
            _repository = repository;
            _mapper = mapper;
            _urlHelper = iUrlHelper;
        }

        
    }
}
