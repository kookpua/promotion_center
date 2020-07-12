using Huatek.Torch.Domain.Abstractions;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Huatek.Torch.Promotions.Models
{
    public class PromotionDto 
    {

        public int Id { get; set; }
        /// <summary>
        /// 活动名称
        /// </summary>
        [StringLength(50, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 1)]
        public string Title { get; set; }

        /// <summary>
        /// 活动描述
        /// </summary>
        [StringLength(1000, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 1)]
        public string Description { get; set; }
        /// <summary>
        /// 限时折扣1,新用户专享2
        /// </summary>
        [Range(1, 2, ErrorMessage = "{0} length must be between {2} and {1}.")]
        public int PromotionTypeId { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [Required]
        //[Display(Name = "Created CustomerId")]
        public int CreatedCustomerId { get; set; }
        /// <summary>
        /// 活动开启时间
        /// </summary>
        [Required]
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 活动结束时间
        /// </summary>
        [Required]
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 新创建为1
        /// </summary>
        [Required]
        public int PromotionStateId { get; set; }

        [DefaultValue(false)]
        public bool Deleted { get; set; }
        /// <summary>
        /// 可设置多个商品1,只能设置一个商品2,全部商品3
        /// </summary>
        [Required]
        public int PromotionProductTypeId { get; set; }



    }
}