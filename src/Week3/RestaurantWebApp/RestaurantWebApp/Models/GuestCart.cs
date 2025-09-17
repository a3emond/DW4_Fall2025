using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleWebApp.Models
{
    public class GuestCart
    {
        public string GuestName { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }
}