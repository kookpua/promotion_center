using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionCenter.Models
{
    public class PromotionProduct
    {
        public int Id { get; set; }
        public int PromotionId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int? StockQuantity { get; set; }
        public bool Deleted { get; set; }
    }
}
