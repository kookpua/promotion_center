using Huatek.Torch.Promotion.Domain.PromotionAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Huatek.Torch.Promotion.Service
{
    public interface IPromotionService
    {
        Task<List<Promotions>> GetAllAsync();
        Task<Promotions> AddPromotionAsync(Promotions entity);

        Task<bool> RemovePromotionAsync(Promotions entity);

        Task<bool> DeleteByIdAsync(int id);

        Task<Promotions> GetPromotionByIdAsync(int id);

        Task<Promotions> UpdatePromotionAsync(Promotions entity);

    }
}
