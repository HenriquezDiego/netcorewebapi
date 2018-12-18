using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Netcorewebapi.DataAccess.Core;
using System.Linq;

namespace Netcorewebapi.Controllers
{
    [Route("api/orders/{orderid}/items")]
    public class OrderItemsController : ControllerBase
    {
        private readonly IRepository _repository;

        public OrderItemsController(IRepository repository,IMapper mapper)
        {
            _repository = repository;
            
        }
       
        [HttpGet]
        public IActionResult Get(int orderId) {

            var order = _repository.GetOrderById(orderId);
            if (order != null)
            {
                return Ok(order.Items);
            }
            return NotFound();

        }

        [HttpGet("{id}")]
        public IActionResult Get(int orderId, int id)
        {

            var order = _repository.GetOrderById(orderId);
            if (order == null) return NotFound();
            var item = order.Items
                .FirstOrDefault(i => i.Id == id);
            if(item != null) return Ok(item);
            return NotFound();

        }
    }
}
