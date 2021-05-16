using Promotions.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Promotions.Core.Dto
{
    public class CartDto
    {
        public List<CartItem> CartItems { get; set; }
    }

    public class CartItemDto
    {
        private int _unitPrice;
        public Code CartItemCode { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get { if (_unitPrice != 0) { return FetchUnitPrice(); } return _unitPrice; } }
        private int FetchUnitPrice()
        {
            switch (CartItemCode)
            {
                case Code.A:
                    return 50;
                case Code.B:
                    return 30;
                case Code.C:
                    return 50;
                case Code.D:
                    return 30;
                default:
                    return 0;
            }
        }
    }
}
