using Huatek.Torch.Promotions.Domain.PromotionAggregate;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Huatek.Torch.Promotions.APP.ViewModel
{
    public class PromotionProductOptionViewModel
    {
        public PromotionProductOptionViewModel()
        {
            PromotionProducts = new List<PromotionProduct>();
        }
        public List<PromotionProduct> PromotionProducts { get;  set; }
        public int PromotionId { get; set; }
        public string Title { get; set; }
        public bool IsAdd { get; set; }

    }
}
