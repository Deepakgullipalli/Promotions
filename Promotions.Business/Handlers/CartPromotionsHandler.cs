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
            var result = await ProcessEligiblePromotionsOnCart(cartDto);
            return new CartValueResponse { Message = $"Updated Cart Value is {result}" };
        }

        private async Task<int> ProcessEligiblePromotionsOnCart(CartDto cart)
        {
            var eligiblePromotions = await _promotionsRepository.FetchEligiblePromotions(cart.CartItems);
            var orderedMaxDiscountPromotions = eligiblePromotions.OrderByDescending(x => x.DifferenceValue);
            int totalValue = 0;
            CartDto leftOverCart = new CartDto();
            HashSet<Code> IsPromotionSetOnItemCodeSet = new HashSet<Code>();
            foreach (var promotion in orderedMaxDiscountPromotions)
            {
                var promotionItems = promotion.PromotionItems;
                foreach (var item in promotionItems)
                {
                    if (!IsPromotionSetOnItemCodeSet.Add(item.CartItemCode))
                    {
                        promotion.CanPromotionBeApplied = false;
                        totalValue = cart.CartValue;
                        break;
                    }
                }
                
                var unmatchedItems = cart.CartItems.Select(x => x.CartItemCode).ToList().Except(promotionItems.Select(y => y.CartItemCode)).ToList();
                if(!promotionItems.Select(y => y.CartItemCode).ToList().Any( item => unmatchedItems.Contains(item)) && unmatchedItems.Count > 0)
                {
                    promotion.CanPromotionBeApplied = false;
                    totalValue = cart.CartValue;
                    break;
                }
                if (promotion.CanPromotionBeApplied)
                {
                    var cartItems = leftOverCart.CartItems ?? cart.CartItems;
                    foreach (var cartItem in cartItems)
                    {
                        var promotional = promotionItems.Where(x => x.CartItemCode == cartItem.CartItemCode).FirstOrDefault();
                        var actualQty = cartItem.Quantity;
                        leftOverCart.CartItems.Clear();
                        leftOverCart.CartItems.Add(new CartItemDto(cartItem.CartItemCode) { Quantity = actualQty - promotional.Quantity });
                        totalValue += (promotion.PromotionPrice + (actualQty - promotional.Quantity) * cartItem.UnitPrice);
                    }
                }

            }
            return totalValue;
        }

        private CartDto MapCartQueryToDto(CartQuery cart)
        {
            var cartDto = new CartDto();
            cartDto.CartItems = new List<CartItemDto>();
            foreach (var item in cart.CartItems)
            {
                var transformedItem = new CartItemDto(item.CartItemCode) { Quantity = item.Quantity };
                cartDto.CartItems.Add(transformedItem);
                cartDto.CartValue += transformedItem.UnitPrice * transformedItem.Quantity;
            }
            return cartDto;
        }
    }
}
