using Huatek.Torch.Promotion.Domain.PromotionAggregate;
using Huatek.Torch.Promotion.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Huatek.Torch.Promotion.Service
{
    public class PromotionService:IPromotionService
    {
        private readonly PromotionContext _context;
        public PromotionService(PromotionContext context)
        {
            _context = context;
        }
        public List<Promotions> GetAllPromotions()
        {
            return _context.Promotions.ToList();
        }
    }
}
