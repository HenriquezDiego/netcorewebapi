using Microsoft.AspNetCore.Mvc;
using NetcorewebApi.DataAccess.Core;

namespace Netcorewebapi.Api.Controllers
{
    [Route("api/orders/{orderid}/items")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderRepository _repository;

        public OrderItemsController(IOrderRepository repository)
        {
            _repository = repository;
        }
       
        [HttpGet]
        public IActionResult Get(int orderId) {

            var order = _repository.GetOrderItems(orderId);
            if (order != null)
            {
                return Ok(order.Items);
            }
            return NotFound();

        }

        [HttpGet("{id}")]
        public IActionResult Get(int orderId, int id)
        {

            var item = _repository.GetItemByOrderId(orderId,id);
            if (item == null) return NotFound();
            return Ok(item);

        }
    }
}
