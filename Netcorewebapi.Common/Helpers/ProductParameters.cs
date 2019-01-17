using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netcorewebapi.Common.Helpers
{
    public class ProductParameters : ResourceParameters
    {
        public string Category { get; set; }
        public string SearchQuery { get; set; }
    }
}
