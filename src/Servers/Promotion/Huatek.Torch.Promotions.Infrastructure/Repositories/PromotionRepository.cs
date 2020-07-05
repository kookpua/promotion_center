using Huatek.Torch.Infrastructure.Core;
using Huatek.Torch.Promotions.Domain.PromotionAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huatek.Torch.Promotions.Infrastructure.Repositories
{
    public class PromotionRepository : Repository<Promotion, int, PromotionContext>,
        IPromotionRepository
    {
        private readonly PromotionContext _context;
        public PromotionRepository(PromotionContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Promotion>> GetPromotionsAsync()
        {
            return await _context.Promotions.ToListAsync();
        }

        public async Task<IEnumerable<Promotion>> GetPromotionsAsync(IEnumerable<int> promotionIds)
        {
            return await _context.Promotions
                .Where(x => promotionIds.Contains(x.Id))
                .OrderBy(x => x.Id)
                .ToListAsync();
        }


        public async Task<IEnumerable<PromotionProduct>> GetPromotionProductsAsync(int promotionId)
        {
            if (promotionId == 0)
            {
                throw new ArgumentNullException(nameof(promotionId));
            }

            return await _context.PromotionProducts
                .Where(x => x.PromotionId == promotionId)
                .OrderBy(x => x.Id)
                .ToListAsync();
        }



    }
}
