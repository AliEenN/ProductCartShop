using ProductCartShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCartShop.ViewModels
{
    public class CartDetailsViewModel
    {
        public Product Product { get; set; }
        public int Count { get; set; }
        public decimal OrderTotal { get; set; }
        public decimal TotalAndShippingRate { get; set; }
        public decimal TotalAfterTax { get; set; }
    }
}
