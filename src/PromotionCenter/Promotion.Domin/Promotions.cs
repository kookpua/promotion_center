using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Promotion.Domin
{
    public class Promotions
    {
        public Promotions()
        {
            PromotionProducts = new List<PromotionProduct>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required, MaxLength(1000)]
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
/*
 只要date的声明方式：
 [Colum(TypeName="date")]
 */