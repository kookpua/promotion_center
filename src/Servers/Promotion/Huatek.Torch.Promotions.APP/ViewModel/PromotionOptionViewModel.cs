using Huatek.Torch.Promotions.API.ViewModel;
using Huatek.Torch.Promotions.Domain.PromotionAggregate;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Huatek.Torch.Promotions.APP.ViewModel
{
    public class PromotionOptionViewModel
    {
        public PromotionOptionViewModel()
        {
            PromotionTypes = new List<SelectListItem>() ;
            PromotionProductTypes = new List<SelectListItem>();
            PromotionStates = new List<SelectListItem>();
        }
        public List<Promotion> Promotions { get;  set; }
        public List<SelectListItem> PromotionTypes { get; set; }
        public List<SelectListItem> PromotionProductTypes { get; set; }
        public List<SelectListItem> PromotionStates { get; set; }
        [Display(Name = "活动名称")]
        public string SearchString { get; set; }
        [Display(Name = "活动类型")]
        public int PromotionTypeId { get; set; }
        [Display(Name = "商品类型")]
        public int PromotionProductTypeId { get; set; }
        [Display(Name = "活动状态")]
        public int PromotionStateId { get; set; }

    }
}
