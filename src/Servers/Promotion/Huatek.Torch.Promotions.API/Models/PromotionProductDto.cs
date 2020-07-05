using Huatek.Torch.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Huatek.Torch.Promotions.Domain.PromotionAggregate
{
    public class PromotionProductDto : Entity<int>
    {
        public int ProductId { get; set; }

        public decimal? Price { get; set; }

        public int? StockQuantity { get; set; }
    }
}
