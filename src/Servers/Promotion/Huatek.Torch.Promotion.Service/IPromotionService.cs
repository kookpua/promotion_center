using Huatek.Torch.Promotion.Domain.PromotionAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Huatek.Torch.Promotion.Service
{
    public interface IPromotionService
    {
        Task<List<Promotions>> GetAllAsync();
    }
}
