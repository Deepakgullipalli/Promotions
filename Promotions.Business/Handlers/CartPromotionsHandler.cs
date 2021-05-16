using Promotions.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Promotions.Business.Handlers
{
    public class CartPromotionsHandler : ICartPromotionsHandler
    {
        public Task<CartValueResponse> FetchCartValue(CartQuery cart)
        {
            throw new NotImplementedException();
        }
    }
}
