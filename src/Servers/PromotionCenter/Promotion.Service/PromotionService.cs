using Promotion.Data;
using Promotion.Domin;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Promotion.Services
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
