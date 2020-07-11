using Huatek.Torch.Domain.Abstractions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Huatek.Torch.Promotions.Domain.PromotionAggregate
{
    public class PromotionProductDto
    {
        public int Id { get; set; }

        [Required]
        public int PromotionId { get; set; }
        [Required]
        public int ProductId { get; set; }

        public decimal? Price { get; set; }

        public int? StockQuantity { get; set; }

        [DefaultValue(false)]
        public bool Deleted { get; set; }
    }
}
