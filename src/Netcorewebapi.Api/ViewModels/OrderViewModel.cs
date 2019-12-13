using System;
using System.Collections.Generic;

namespace Netcorewebapi.Api.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate{ get; set; }
        public string OrderNumber { get; set; }
        public IEnumerable<OrderItemViewModel> Items { get; set; }
    }
}
