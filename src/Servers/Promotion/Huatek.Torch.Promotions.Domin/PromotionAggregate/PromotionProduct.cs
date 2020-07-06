using Huatek.Torch.Domain.Abstractions;
using System.ComponentModel;

namespace Huatek.Torch.Promotions.Domain.PromotionAggregate
{
    public class PromotionProduct : Entity<int>, IAggregateRoot
    {
        public int PromotionId { get; set; }
        public int ProductId { get; set; }

        //[Column(TypeName = "decimal(18,4)")]
        public decimal? Price { get; set; }

        public int? StockQuantity { get; set; }
        [DefaultValue(false)]
        public bool Deleted { get; set; }
        public virtual Promotion Promotion { get; set; }
    }
}
