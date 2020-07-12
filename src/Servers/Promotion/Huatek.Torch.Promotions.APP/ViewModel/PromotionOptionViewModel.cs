using Huatek.Torch.Promotions.APP.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Huatek.Torch.Promotions.APP.ViewModel
{
    public class PromotionOptionViewModel
    {
        public PromotionOptionViewModel()
        {
            PromotionTypes = new List<SelectListItem>() ;
            PromotionProductTypes = new List<SelectListItem>();
            PromotionStates = new List<SelectListItem>();
            PromotionDtos = new List<PromotionDto>();
        }
        public List<PromotionDto> PromotionDtos { get;  set; }
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
