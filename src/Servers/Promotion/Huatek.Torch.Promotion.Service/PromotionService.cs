using Huatek.Torch.Promotion.Domain.PromotionAggregate;
using Huatek.Torch.Promotion.Infrastructure;
using Huatek.Torch.Promotion.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huatek.Torch.Promotion.Service
{
    public class PromotionService:IPromotionService
    {
        private readonly IPromotionRepository _promotionRepository;
        private readonly PromotionContext _dbContext;
        public PromotionService(IPromotionRepository promotionRepository,
            PromotionContext dbContext)
        {
            _promotionRepository = promotionRepository;
            _dbContext = dbContext;
        }
        /// <summary>
        /// 获取所有的促销活动
        /// </summary>
        /// <returns></returns>
        public async Task<List<Promotions>> GetAllAsync()
        {
            return await Task.Run<List<Promotions>>(() =>
            {
                return _dbContext.Promotions.ToList();
            });
        }
      
        public async Task<Promotions> AddPromotionAsync(Promotions entity)
        {
            return await _promotionRepository.AddAsync(entity);
        }

        public async Task<bool> RemovePromotionAsync(Promotions entity)
        {
            return await _promotionRepository.RemoveAsync(entity);
        }
        public async Task<bool> DeleteByIdAsync(int id)
        {
            return await _promotionRepository.DeleteAsync(id);
        }

        public async Task<Promotions> GetPromotionByIdAsync(int id)
        {
            return await _promotionRepository.GetAsync(id);
        }

        public async Task<Promotions> UpdatePromotionAsync(Promotions entity)
        {
            return await _promotionRepository.UpdateAsync(entity);
        }
    }
}
