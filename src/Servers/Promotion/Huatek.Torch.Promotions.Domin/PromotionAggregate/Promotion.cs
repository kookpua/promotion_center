using Huatek.Torch.Domain.Abstractions;
using Huatek.Torch.Promotions.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Huatek.Torch.Promotions.Domain.PromotionAggregate
{
    public class Promotion : Entity<int>, IAggregateRoot
    {
        public Promotion()
        {
            PromotionProducts = new List<PromotionProduct>();
        }

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
        /// 活动类型：限时折扣1,新用户专享2
        /// </summary>
        [Range(1, 2, ErrorMessage = "请输入正确的活动类型.")]
        public int PromotionTypeId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public PromotionType PromotionType
        {
            get { return (PromotionType)PromotionTypeId; }
            set { PromotionTypeId = (int)value; }
        }

        [JsonIgnore]
        public DateTime CreatedOnUtc { get; set; } = DateTime.Now;
        [Range(1,int.MaxValue,ErrorMessage = "用户ID不能为0")]
        public int CreatedCustomerId { get; set; }
        [DataType(DataType.DateTime, ErrorMessage = "请输入正确的时间")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.DateTime, ErrorMessage = "请输入正确的时间")]
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 活动状态：1,2,3,4
        /// </summary>
        [Range(1,4,ErrorMessage = "请输入正确的活动状态.")]
        public int PromotionStateId { get; set; }
        [NotMapped]
        [JsonIgnore]
        public PromotionState PromotionState
        {
            get { return (PromotionState)PromotionStateId; }
            set { PromotionStateId = (int)value; }
        }
        [DefaultValue(false)]
        public bool Deleted { get; set; }
        /// <summary>
        /// 活动商品类型：可设置多个商品1,只能设置一个商品2,全部商品3
        /// </summary>
        [Range(1, 3, ErrorMessage = "请输入正确的活动商品类型.")]
        public int PromotionProductTypeId { get; set; }
        [NotMapped]
        [JsonIgnore]
        public PromotionProductType PromotionProductType
        {
            get { return (PromotionProductType)PromotionProductTypeId; }
            set { PromotionProductTypeId = (int)value; }
        }
        [NotMapped]
        //[JsonIgnore]
        public virtual ICollection<PromotionProduct> PromotionProducts { get; set; }
    }
}