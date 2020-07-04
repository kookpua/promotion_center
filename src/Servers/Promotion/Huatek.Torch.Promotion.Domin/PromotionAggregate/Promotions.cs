using Huatek.Torch.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Huatek.Torch.Promotion.Domain.PromotionAggregate
{
    public class Promotions : Entity<int>, IAggregateRoot
    {
        public Promotions()
        {
            PromotionProducts = new List<PromotionProduct>();
        }

        //[Required]
        //[MaxLength(50)]
        public string Title { get; set; }

        //[Required, MaxLength(1000)]
        public string Description { get; set; }

        public int PromotionTypeId { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public int CreatedCustomerId { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public int UpdatedCustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PromotionStateId { get; set; }
        public bool Deleted { get; set; }
        public int PromotionProductTypeId { get; set; }
        public List<PromotionProduct> PromotionProducts { get; set; }
    }
}