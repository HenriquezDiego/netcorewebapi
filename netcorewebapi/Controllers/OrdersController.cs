using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Netcorewebapi.DataAccess.Core;
using Netcorewebapi.DataAccess.Data.Entities;
using Netcorewebapi.ViewModels;
using System;
using System.Collections.Generic;

namespace Netcorewebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public OrdersController(IRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(bool include = true)
        {
            try
            {
                var result = _repository.GetAllOrders(include);
                return Ok(_mapper.Map<IEnumerable<Order>,IEnumerable<OrderViewModel>>(result));
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to get Orders, OnlyTest: {ex}");
            }

        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var order = _repository.GetOrderById(id);
                if (order != null)
                    return Ok(_mapper.Map<Order,OrderViewModel>(order));
                  
                return NotFound();

            }
            catch (System.Exception ex)
            {
                return BadRequest($"Failed to get Orders {ex}");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]Order model) {

            try
            {
                if (ModelState.IsValid)
                {
                    _repository.AddEntityOrder(model);
                    return Created($"/api/orders/{model.Id}", model);

                }

               
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to Post Orders, OnlyTest: {ex}");
                
            }

            return BadRequest("Failed to save new Order");
            
        }


        


    }
}
