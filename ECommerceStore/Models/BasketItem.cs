using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ECommerceStore.Models
{
    public class BasketItem
    {
        [Key]
        public int Id{ get; set; }
        public int Quantity { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public string BasketId { get; set; }
        public virtual Basket Basket { get; set; }


    }
}
