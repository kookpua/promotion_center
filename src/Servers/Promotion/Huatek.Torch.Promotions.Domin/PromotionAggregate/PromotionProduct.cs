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
        public int ProductId { get; set; }

        //[Column(TypeName = "decimal(18,4)")]
        public decimal? Price { get; set; }

        public int? StockQuantity { get; set; }

        [JsonIgnore]
        [DefaultValue(false)]
        public bool Deleted { get; set; }
        [NotMapped]
        //[JsonIgnore]
        public virtual Promotion Promotion { get; set; }
    }
}
