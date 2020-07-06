using Huatek.Torch.Domain.Abstractions;

namespace Huatek.Torch.Promotions.Domain.PromotionAggregate
{
    public class PromotionDto : Entity<int>
    {
       
        public string PromotionTitle { get; set; }

        public string Description { get; set; }
      
        public int PromotionTypeId { get; set; }

      
    }
}