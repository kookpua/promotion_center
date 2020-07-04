using Huatek.Torch.Infrastructure.Core;
using Huatek.Torch.Promotion.Domain.PromotionAggregate;

namespace Huatek.Torch.Promotion.Infrastructure.Repositories
{
    public class PromotionRepository : Repository<Promotions, int, PromotionContext>,
        IPromotionRepository
    {
        public PromotionRepository(PromotionContext context) : base(context)
        {
        }
    }
}
