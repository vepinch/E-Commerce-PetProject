using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ECommerceStore.Models;
using ECommerceStore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using ECommerce.Data;

namespace ECommerceStore.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            this.db = db;
        }

       
        public IActionResult Index()
        {
            return View(db.Products.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Buy(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            //HttpContext.Session.SetString("product", JsonSerializer.Serialize(db.Products.Find(id)));
            // HttpContext.Session.Clear();
            
            if (HttpContext.Session.GetString("ProductsAmount")!=null)
            {
                int ProductAmount = Int32.Parse(HttpContext.Session.GetString("ProductsAmount"));
                ProductAmount++;
                HttpContext.Session.Set<Product>("Product"+ProductAmount.ToString(), db.Products.Find(id));
                HttpContext.Session.SetString("ProductsAmount", ProductAmount.ToString());
            }
            else
            {
                HttpContext.Session.SetString("ProductsAmount", "1");
                HttpContext.Session.Set<Product>("Product1", db.Products.Find(id));
            }
            return RedirectToAction("Index");
            //return RedirectToAction("AddToBasket", "Basket",db.Products.Find(id)); 
        }
    }
}
