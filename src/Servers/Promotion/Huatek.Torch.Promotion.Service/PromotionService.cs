using Huatek.Torch.Promotion.Domain.PromotionAggregate;
using Huatek.Torch.Promotion.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huatek.Torch.Promotion.Service
{
    public class PromotionService:IPromotionService
    {
        private readonly PromotionContext _context;
        public PromotionService(PromotionContext context)
        {
            _context = context;
        }
        /// <summary>
        /// 获取所有的促销活动
        /// </summary>
        /// <returns></returns>
        public async Task<List<Promotions>> GetAllAsync()
        {
            return await Task.Run<List<Promotions>>(() =>
            {
                return _context.Promotions.ToList();
            });
        }
    }
}
