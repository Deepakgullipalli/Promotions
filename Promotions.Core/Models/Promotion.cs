using Promotions.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Promotions.Core.Models
{
    public class Promotion
    {
        public List<CartItemDto> PromotionItems { get; set; }
        public bool IsPromotionActive { get; set; }
        public int PromotionPrice { get; set; }
        public int DifferenceValue { get; set; }
        public bool CanPromotionBeApplied { get; set; }
        public bool IsPromotionApplied { get; set; }
    }
}
