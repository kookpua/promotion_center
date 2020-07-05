using Huatek.Torch.Domain;
using Huatek.Torch.Promotions.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Huatek.Torch.Promotions.Domain.PromotionAggregate
{
    public class PromotionDto : Entity<int>
    {
       
        public string PromotionTitle { get; set; }

        public string Description { get; set; }
      
        public int PromotionTypeId { get; set; }

      
    }
}