using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleWebApp.Models
{
    public class CartItem
    {
        public int MenuItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}