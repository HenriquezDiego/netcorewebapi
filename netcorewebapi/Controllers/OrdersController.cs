using Microsoft.AspNetCore.Mvc;
using Netcorewebapi.DataAccess.Core;
using Netcorewebapi.DataAccess.Data.Entities;
using System;

namespace Netcorewebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IRepository _repository;

        public OrdersController(IRepository repository)
        {
            _repository = repository;
        
        }

        [HttpGet]
        public IActionResult Get(bool include = true)
        {
            try
            {
                var result = _repository.GetAllOrders(include);
                return Ok(result);
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
                    return Ok(order);
                  
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
