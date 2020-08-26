using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ECommerce.Data;
using Microsoft.EntityFrameworkCore;
using ECommerceStore.Data;

namespace ECommerceStore.Controllers
{
    public class BasketController : Controller
    {
        private readonly ApplicationDbContext db;

        public BasketController(ApplicationDbContext db)
        {
            this.db = db;
        }
        
        private Basket GetBasket()  // Get filled basket from cookies
        {
            Basket basket = new Basket();
            if (HttpContext.Session.GetString("ProductsAmount") != null)
            {
                int ProductAmount = Int32.Parse(HttpContext.Session.GetString("ProductsAmount"));
                for (int i = 1; i <= ProductAmount; i++)
                {
                    basket.AddBasketItem(HttpContext.Session.Get<Product>("Product" + i.ToString()));
                }

            }
            return basket;
        }
        public IActionResult Index()
        {
            Basket basket = GetBasket();
            return View(basket.BasketItems.ToList());
        }

        public IActionResult AddToBasket(Product prod)
        {
            Basket basket = new Basket();
            basket.AddBasketItem(prod);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Privacy","Home");
            }

            Basket basket = GetBasket();

            //Deleting from Basket List
            basket.DeleteItem(id);


            //Deleting from session
            int j = 1;
            HttpContext.Session.Clear();
            foreach(var item in basket.BasketItems)
            {
                for (int i = 0; i < item.Quantity; i++)
                {
                    HttpContext.Session.Set<Product>("Product" + j.ToString(), item.Product);
                    j++;
                }
                
            }
            HttpContext.Session.SetString("ProductsAmount", basket.Count.ToString());
            
            return View("Index", basket.BasketItems.ToList());
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(string User, string Address,string ContactPhone)
        {
            Basket basket = GetBasket();
            string basketItems = "";
            foreach (var item in basket.BasketItems)
            {
                basketItems += item.Product.Id.ToString() + ";" + item.Product.Name.ToString() +";"+ item.Quantity.ToString()+";";
            }
            basketItems += basket.Count.ToString() + ";" + basket.Price.ToString();
            Order order = new Order()
            {
                User = User,
                Address = Address.Trim(),
                ContactPhone = ContactPhone.Trim(),
                Basket = basketItems,
                BasketId = basket.Id,
                OrderId = db.Orders.Count()+1
            };

            db.Orders.Add(order);
            db.SaveChanges();
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
           
        }
    }
}