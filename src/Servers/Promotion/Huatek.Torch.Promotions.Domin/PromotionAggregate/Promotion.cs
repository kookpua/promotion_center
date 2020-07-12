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
        public string Title { get; set; }

        /// <summary>
        /// 活动描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 活动类型：限时折扣1,新用户专享2
        /// </summary>
        public int PromotionTypeId { get; set; }

        [NotMapped]
        public PromotionType PromotionType
        {
            get { return (PromotionType)PromotionTypeId; }
            set { PromotionTypeId = (int)value; }
        }

        public DateTime CreatedOnUtc { get; set; } = DateTime.Now;
        public int CreatedCustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 活动状态：1,2,3,4
        /// </summary>
        [Range(1,4,ErrorMessage = "活动状态必填.")]
        [Display(Name = "活动状态")]
        [DefaultValue(1)]
        public int PromotionStateId { get; set; }
        [NotMapped]
        public PromotionState PromotionState
        {
            get { return (PromotionState)PromotionStateId; }
            set { PromotionStateId = (int)value; }
        }
        public bool Deleted { get; set; }
        public int PromotionProductTypeId { get; set; }
        [NotMapped]
        public PromotionProductType PromotionProductType
        {
            get { return (PromotionProductType)PromotionProductTypeId; }
            set { PromotionProductTypeId = (int)value; }
        }
        [NotMapped]
        public virtual ICollection<PromotionProduct> PromotionProducts { get; set; }
    }
}