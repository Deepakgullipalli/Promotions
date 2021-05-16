using System;
using System.Collections.Generic;
using System.Text;

namespace Promotions.Core.Models
{
    public class CartQuery
    {
        public List<CartItem> CartItems { get; set; }
    }

    public class CartItem
    {
        public Code CartItemCode { get; set; }
        public int Quantity { get; set; }
    }

    public enum Code
    {
        A,
        B,
        C,
        D,
        E
    }
}
