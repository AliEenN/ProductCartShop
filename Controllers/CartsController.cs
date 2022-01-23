using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductCartShop.Models;
using ProductCartShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCartShop.Controllers
{
    public class CartsController : Controller
    {
        public CartsController()
        {
        }

        // GET
        [HttpGet]
        public IActionResult Index()
        {
            var cartDetailsVM = new CartDetailsViewModel()
            {
                OrderTotal = 0,
                Count = HttpContext.Session.GetInt32("count") == null ? 0 : (int)HttpContext.Session.GetInt32("count")
            };

            var str = HttpContext.Session.GetString("product");

            if (str == null)
                return View(cartDetailsVM);

            var obj = JsonConvert.DeserializeObject<Product>(str);

            cartDetailsVM.Product = obj;

            if (cartDetailsVM.Product.Description.Length > 75)
            {
                cartDetailsVM.Product.Description = cartDetailsVM.Product.Description.Substring(0, 74);
            }

            cartDetailsVM.OrderTotal = cartDetailsVM.Product.Price * cartDetailsVM.Count;
            cartDetailsVM.TotalAndShippingRate = cartDetailsVM.Product.ShippingRate * cartDetailsVM.Count + cartDetailsVM.OrderTotal;

            return View(cartDetailsVM);
        }

        // POST
        [HttpPost]
        public IActionResult Plus()
        {
            var str = HttpContext.Session.GetString("product");
            var obj = JsonConvert.DeserializeObject<Product>(str);

            var cartDetailsVM = new CartDetailsViewModel()
            {
                Count = (int)HttpContext.Session.GetInt32("count") + 1,
                Product = obj
            };

            cartDetailsVM.OrderTotal = obj.Price * cartDetailsVM.Count;
            cartDetailsVM.TotalAndShippingRate = cartDetailsVM.Product.ShippingRate * cartDetailsVM.Count + cartDetailsVM.OrderTotal;

            HttpContext.Session.SetInt32("count", cartDetailsVM.Count);

            return View("Index", cartDetailsVM);
        }

        // POST
        [HttpPost]
        public IActionResult Minus()
        {
            var str = HttpContext.Session.GetString("product");
            var obj = JsonConvert.DeserializeObject<Product>(str);

            var cartDetailsVM = new CartDetailsViewModel()
            {
                Count = (int)HttpContext.Session.GetInt32("count") - 1 == 0 ? (int)HttpContext.Session.GetInt32("count") : (int)HttpContext.Session.GetInt32("count") - 1,
                Product = obj
            };

            cartDetailsVM.OrderTotal = obj.Price * cartDetailsVM.Count;
            cartDetailsVM.TotalAndShippingRate = cartDetailsVM.Product.ShippingRate * cartDetailsVM.Count + cartDetailsVM.OrderTotal;

            HttpContext.Session.SetInt32("count", cartDetailsVM.Count);

            return View("Index", cartDetailsVM);
        }
        
        // GET
        [HttpGet]
        public IActionResult Continue()
        {
            var str = HttpContext.Session.GetString("product");
            var obj = JsonConvert.DeserializeObject<Product>(str);

            var cartDetailsVM = new CartDetailsViewModel()
            {
                Count = (int)HttpContext.Session.GetInt32("count"),
                Product = obj
            };

            cartDetailsVM.OrderTotal = cartDetailsVM.Product.Price * cartDetailsVM.Count;
            cartDetailsVM.TotalAndShippingRate = cartDetailsVM.Product.ShippingRate * cartDetailsVM.Count + cartDetailsVM.OrderTotal;
            cartDetailsVM.TotalAfterTax = cartDetailsVM.TotalAndShippingRate * (decimal)0.14 + cartDetailsVM.TotalAndShippingRate;

            return View(cartDetailsVM);
        }
    }
}
