using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Promotion.Domin
{
    public class PromotionProduct
    {
        public int Id { get; set; }
        public int PromotionId { get; set; }
        public int ProductId { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        public int? StockQuantity { get; set; }
        public bool Deleted { get; set; }
    }
}
