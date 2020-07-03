using Promotion.Domin;
using System;
using System.Collections.Generic;

namespace Promotion.Services
{
    public interface IPromotionService
    {
        List<Promotions> GetAllPromotions();
    }
}
