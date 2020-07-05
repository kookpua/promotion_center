using Huatek.Torch.Infrastructure.Core;
using Huatek.Torch.Promotions.Domain.PromotionAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Huatek.Torch.Promotions.Infrastructure.Repositories
{
    public interface IPromotionRepository : IRepository<Promotion, int>
    {
        Task<IEnumerable<Promotion>> GetPromotionsAsync();

        Task<IEnumerable<PromotionProduct>> GetPromotionProductsAsync(int promotionId);
    }
}
