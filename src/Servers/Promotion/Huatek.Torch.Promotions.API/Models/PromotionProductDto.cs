using Huatek.Torch.Domain.Abstractions;

namespace Huatek.Torch.Promotions.Domain.PromotionAggregate
{
    public class PromotionProductDto : Entity<int>
    {
        public int ProductId { get; set; }

        public decimal? Price { get; set; }

        public int? StockQuantity { get; set; }
    }
}
