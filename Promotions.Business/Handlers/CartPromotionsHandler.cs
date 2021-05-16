using Promotions.Core.Dto;
using Promotions.Core.Models;
using Promotions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promotions.Business.Handlers
{
    public class CartPromotionsHandler : ICartPromotionsHandler
    {
        private readonly IPromotionsRepository _promotionsRepository;
        public CartPromotionsHandler(IPromotionsRepository promotionsRepository)
        {
            _promotionsRepository = promotionsRepository;
        }
        public async Task<CartValueResponse> FetchCartValue(CartQuery cart)
        {
            if (!cart.CartItems.Any())
            {
                return new CartValueResponse { Message = "Cart does not contain any items" };
            }

            var cartDto = MapCartQueryToDto(cart);
            await _promotionsRepository.FetchAllPromotions();
            throw new NotImplementedException();
        }

        private CartDto MapCartQueryToDto(CartQuery cart)
        {
            var cartDto = new CartDto();
            cartDto.CartItems = new List<CartItemDto>();
            foreach (var item in cart.CartItems)
            {
                cartDto.CartItems.Add(new CartItemDto(item.CartItemCode) { Quantity = item.Quantity });
            }
            return cartDto;
        }
    }
}
