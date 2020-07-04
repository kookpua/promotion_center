using Huatek.Torch.Promotion.Domain.PromotionAggregate;
using System;
using System.Collections.Generic;

namespace Huatek.Torch.Promotion.Service
{
    public interface IPromotionService
    {
        List<Promotions> GetAllPromotions();
    }
}
