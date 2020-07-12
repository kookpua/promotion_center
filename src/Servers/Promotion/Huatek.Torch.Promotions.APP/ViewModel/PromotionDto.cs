using Huatek.Torch.Domain.Abstractions;
using Huatek.Torch.Promotions.Domain.Enum;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Huatek.Torch.Promotions.APP.ViewModel
{
    public class PromotionDto 
    {


        public int Id { get; set; }
        /// <summary>
        /// 活动名称
        /// </summary>
        [Required(ErrorMessage = "活动名称必填.")]
        [MaxLength(50, ErrorMessage = "活动名称最大只能输入{1}.")]
        [Display(Name = "活动名称")]
        public string Title { get; set; }

        /// <summary>
        /// 活动描述
        /// </summary>
        [Required(ErrorMessage = "活动描述必填.")]
        [MaxLength(1000, ErrorMessage = "活动名称最大只能输入{1}.")]
        [Display(Name = "活动描述")]
        public string Description { get; set; }
        /// <summary>
        /// 活动类型：限时折扣1,新用户专享2
        /// </summary>
        [Range(1, 2, ErrorMessage = "活动类型不正确.")]
        [Display(Name = "活动类型")]
        public int PromotionTypeId { get; set; }

        [JsonIgnore]
        public PromotionType PromotionType
        {
            get { return (PromotionType)PromotionTypeId; }
            set { PromotionTypeId = (int)value; }
        }

        [Display(Name = "创建时间")]
        public DateTime CreatedOnUtc { get; set; } = DateTime.Now;
        [Display(Name = "创建人ID")]
        public int CreatedCustomerId { get; set; }
        [Required(ErrorMessage = "开始时间必填.")]
        [Display(Name = "开始时间")]
        public DateTime? StartDate { get; set; }
        [Required(ErrorMessage = "结束时间必填.")]
        [Display(Name = "结束时间")]
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// 活动状态：1,2,3,4
        /// </summary>
        [Display(Name = "活动状态")]
        public int PromotionStateId { get; set; }
        [JsonIgnore]
        public PromotionState PromotionState
        {
            get { return (PromotionState)PromotionStateId; }
            set { PromotionStateId = (int)value; }
        }
        [DefaultValue(false)]
        [Display(Name = "删除")]
        public bool Deleted { get; set; }
        /// <summary>
        /// 活动商品类型：可设置多个商品1,只能设置一个商品2,全部商品3
        /// </summary>
        [Range(1, 3, ErrorMessage = "活动商品类型不正确.")]
        [Display(Name = "商品类型")]
        public int PromotionProductTypeId { get; set; }
        [JsonIgnore]
        public PromotionProductType PromotionProductType
        {
            get { return (PromotionProductType)PromotionProductTypeId; }
            set { PromotionProductTypeId = (int)value; }
        }
    }
}