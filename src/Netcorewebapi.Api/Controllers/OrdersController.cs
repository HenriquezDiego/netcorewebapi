using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Netcorewebapi.Api.ViewModels;
using Netcorewebapi.DataAccess.Entities;
using NetcorewebApi.DataAccess.Core;
using System;
using System.Collections.Generic;

namespace Netcorewebapi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;

        public OrdersController(IOrderRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _repository.GetAll();
            
            if (result == null) return BadRequest();
            return Ok(_mapper.Map<IEnumerable<Order>,IEnumerable<OrderViewModel>>(result));
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var order = _repository.Get(id);
            if (order != null)
                return Ok(_mapper.Map<Order,OrderViewModel>(order));
              
            return NotFound();
        }

        [HttpPost]
        public IActionResult Post([FromBody]Order model) {

            _repository.Add(model);
           
            if (_repository.Save()) return Created(new Uri($"/api/orders/{model?.Id}"), model);
            return BadRequest();
        }

    }
}
