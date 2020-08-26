using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceStore.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string User { get; set; }
        public string Address { get; set; }
        public string ContactPhone { get; set; }
        public string BasketId { get; set; }
        public string Basket { get; set; }

    }
}
