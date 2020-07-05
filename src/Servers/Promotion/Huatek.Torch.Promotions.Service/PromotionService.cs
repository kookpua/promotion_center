using Huatek.Torch.Promotions.Domain.PromotionAggregate;
using Huatek.Torch.Promotions.Infrastructure;
using Huatek.Torch.Promotions.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huatek.Torch.Promotions.Service
{
    public class PromotionService:IPromotionService
    {
        private readonly IPromotionRepository _promotionRepository;
        public PromotionService(IPromotionRepository promotionRepository)
        {
            _promotionRepository = promotionRepository;
        }
        /// <summary>
        /// 获取所有的促销活动
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Promotion>> GetPromotionsAsync()
        {
            return await _promotionRepository.GetPromotionsAsync();
        }
      
        public async Task<Promotion> AddPromotionAsync(Promotion entity)
        {
            return await _promotionRepository.AddAsync(entity);
        }

        public async Task<bool> RemovePromotionAsync(Promotion entity)
        {
            return await _promotionRepository.RemoveAsync(entity);
        }
        public async Task<bool> DeleteByIdAsync(int id)
        {
            return await _promotionRepository.DeleteAsync(id);
        }

        public async Task<Promotion> GetPromotionByIdAsync(int id)
        {
            return await _promotionRepository.GetAsync(id);
        }

        public async Task<Promotion> UpdatePromotionAsync(Promotion entity)
        {
            return await _promotionRepository.UpdateAsync(entity);
        }
    }
}
