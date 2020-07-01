using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionCenter.Models
{
    public class Promotion
    {
        public int Id { get; set; }
        public string Title { get; set; }
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
    }
}
