using Promotions.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Promotions.Repositories
{
    public class PromotionsRepository : IPromotionsRepository
    {
        public async Task<IList<Promotion>> FetchAllPromotions()
        {
            throw new NotImplementedException();
        }
    }
}
