using System;
using System.Collections.Generic;

#nullable disable

namespace ShoppingAPI.Models
{
    public partial class Cart
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public string Username { get; set; }
        public int Quantity { get; set; }
    }
}
