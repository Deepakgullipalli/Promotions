using Promotions.Core.Dto;
using Promotions.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promotions.Repositories
{
    public class PromotionsRepository : IPromotionsRepository
    {
        public async Task<IList<Promotion>> FetchAllPromotions()
        {
            List<Promotion> promotions = new List<Promotion>();
            promotions.Add(new Promotion
            {
                PromotionItems = new List<CartItemDto>() { new CartItemDto(Code.A) { Quantity = 3 } },
                IsPromotionActive = true,
                PromotionPrice = 130
            });
            promotions.Add(new Promotion
            {
                PromotionItems = new List<CartItemDto>() { new CartItemDto(Code.B) { Quantity = 2 } },
                IsPromotionActive = true,
                PromotionPrice = 45
            });
            promotions.Add(new Promotion
            {
                PromotionItems = new List<CartItemDto>() { new CartItemDto(Code.A) { Quantity = 3 },
                new CartItemDto(Code.B) { Quantity = 3 }},
                IsPromotionActive = true,
                PromotionPrice = 130
            });
            promotions.Add(new Promotion
            {
                PromotionItems = new List<CartItemDto>() { new CartItemDto(Code.D) { Quantity = 2 } },
                IsPromotionActive = true,
                PromotionPrice = 20
            });
            promotions.Add(new Promotion
            {
                PromotionItems = new List<CartItemDto>() { new CartItemDto(Code.C) {  Quantity = 1 } ,
                new CartItemDto(Code.D) { Quantity = 1 }},
                IsPromotionActive = true,
                PromotionPrice = 15
            });
            CalculateDifferenceValueForPromotions(promotions);
            return promotions;
        }

        private void CalculateDifferenceValueForPromotions(IList<Promotion> promotions)
        {
            foreach (var promotion in promotions)
            {
                var totalProductValueAtUnitPrice = promotion.PromotionItems.Select(x => x.Quantity * x.UnitPrice).Sum();
                promotion.DifferenceValue = totalProductValueAtUnitPrice - promotion.PromotionPrice;
            }
        }

        public async Task<IList<Promotion>> FetchEligiblePromotions(IList<CartItemDto> cartItems)
        {
            var promotions =await FetchAllPromotions();
            foreach (var item in cartItems)
            {
                foreach (var promotion in promotions)
                {
                    var promotionItem = promotion.PromotionItems.Where(x => x.CartItemCode == item.CartItemCode).FirstOrDefault();
                    if (promotionItem.Quantity <= item.Quantity)
                    {
                        promotion.CanPromotionBeApplied = true;
                        continue;
                    }
                }
            }
            return promotions.Where(x => x.CanPromotionBeApplied = true).ToList();
        }
    }
}
