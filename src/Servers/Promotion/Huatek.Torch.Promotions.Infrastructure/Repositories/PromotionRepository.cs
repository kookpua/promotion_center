using Huatek.Torch.Infrastructure.Core;
using Huatek.Torch.Promotions.Domain.PromotionAggregate;

namespace Huatek.Torch.Promotions.Infrastructure.Repositories
{
    public class PromotionRepository : Repository<Promotion, int, PromotionContext>,
        IPromotionRepository
    {
        public PromotionRepository(PromotionContext context) : base(context)
        {
        }
    }
}
