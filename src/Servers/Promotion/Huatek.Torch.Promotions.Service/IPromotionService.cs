using Huatek.Torch.Promotions.Domain.PromotionAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Huatek.Torch.Promotions.Service
{
    public interface IPromotionService
    {
        Task<IEnumerable<Promotion>> GetPromotionsAsync();
        Task<Promotion> AddPromotionAsync(Promotion entity);

        Task<bool> RemovePromotionAsync(Promotion entity);

        Task<bool> DeleteByIdAsync(int id);

        Task<Promotion> GetPromotionByIdAsync(int id);

        Task<Promotion> UpdatePromotionAsync(Promotion entity);

    }
}
