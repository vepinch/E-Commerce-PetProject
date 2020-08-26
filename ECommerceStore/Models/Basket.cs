using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace ECommerceStore.Models
{
    public class Basket
    {
        [Key]
        public string Id { get; set; }
        public List<BasketItem> BasketItems { get; set; }
        public int Price { get; private set; }
        public int Count { get; private set; }

        public Basket()
        {
            var dateandtime = DateTime.Now;
            Id = dateandtime.ToShortDateString();
            Price = 0;
            Count = 0;
            BasketItems = new List<BasketItem>();
        }
        public void AddBasketItem(Product prod)
        {
            int ItemId = BasketItems.Count()+1;
            if (BasketItems.Count()>0)
            {
                bool contains = false;
                foreach (var item in BasketItems)
                {
                    if (item.ProductId == prod.Id)
                    {
                        item.Quantity++;
                        contains = true;
                    }
                }
                if (!contains)
                {
                    BasketItems.Add(new BasketItem
                    {
                        BasketId = this.Id,
                        Basket = this,
                        Product = prod,
                        ProductId = prod.Id,
                        Quantity = 1
                    });
                }
            }
            else
            {
                BasketItems.Add(new BasketItem
                {
                    BasketId = this.Id,
                    Basket = this,
                    Product = prod,
                    ProductId = prod.Id,
                    Quantity = 1
                });
            }
            


            Price += prod.Price;
            Count += 1;
        }

        public void DeleteItem(int productId)
        {
            foreach (var item in BasketItems)
            {
                if (item.ProductId == productId)
                {
                    Count -= item.Quantity;
                    Price -= (item.Quantity * item.Product.Price);
                    BasketItems.Remove(item);
                    break;
                }
            }
        }
        

    }
}
