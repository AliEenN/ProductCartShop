using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProductCartShop.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCartShop.Controllers
{
    public class HomeController : Controller
    {
        public static List<Product> products;
        public HomeController()
        {
            products = new List<Product>()
            {
                new Product { Id = 1, Name = "Car", Description = "Car Description", Price = 1100, ShippingRate = 75 }
            };
        }

        public IActionResult Index()
        {
            return View(products);
        }

        public IActionResult AddToShoppingCarts(int? id)
        {
            if (id == null)
                return BadRequest();

            var productExist = products.FirstOrDefault(e => e.Id == id);

            if (productExist == null)
                return NotFound();
            
            var key = "product";
            var str = JsonConvert.SerializeObject(productExist);
            HttpContext.Session.SetString(key, str);

            HttpContext.Session.SetInt32("count", 1);

            return RedirectToAction(nameof(Index));
        }
    }
}
