using Huatek.Torch.Domain.Abstractions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Huatek.Torch.Promotions.Domain.PromotionAggregate
{
    public class PromotionProduct : Entity<int>, IAggregateRoot
    {
        public int PromotionId { get; set; }

        [DisplayName("商品ID")]

        public int ProductId { get; set; }

        //[Column(TypeName = "decimal(18,4)")]
        [DisplayName("活动价")]
        public decimal? Price { get; set; }
        [DisplayName("活动库存")]
        public int? StockQuantity { get; set; }

        [DefaultValue(false)]
        [DisplayName("删除")]
        public bool Deleted { get; set; }
        [NotMapped]
        //[JsonIgnore]
        public virtual Promotion Promotion { get; set; }
    }
}
