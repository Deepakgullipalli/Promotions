using Promotions.Core.Dto;
using Promotions.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Promotions.Repositories
{
    public interface IPromotionsRepository
    {
        Task<IList<Promotion>> FetchEligiblePromotions(IList<CartItemDto> cartItems);
    }
}
