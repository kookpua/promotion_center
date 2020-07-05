using Huatek.Torch.Domain;
using Huatek.Torch.Promotions.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// 活动描述
        /// </summary>
        [Required, MaxLength(1000)]
        public string Description { get; set; }
        /// <summary>
        /// 限时折扣1,新用户专享2
        /// </summary>
        public int PromotionTypeId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public PromotionType PromotionType
        {
            get { return (PromotionType)PromotionTypeId; }
            set { PromotionTypeId = (int)value; }
        }
        public DateTime CreatedOnUtc { get; set; }
        public int CreatedCustomerId { get; set; }
        //public DateTime UpdatedOnUtc { get; set; }
        //public int UpdatedCustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 新创建为1
        /// </summary>
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
        /// 可设置多个商品1,只能设置一个商品2,全部商品3
        /// </summary>
        public int PromotionProductTypeId { get; set; }
        [NotMapped]
        [JsonIgnore]
        public PromotionProductType PromotionProductType
        {
            get { return (PromotionProductType)PromotionProductTypeId; }
            set { PromotionProductTypeId = (int)value; }
        }
        [JsonIgnore]
        public virtual ICollection<PromotionProduct> PromotionProducts { get; set; }
    }
}