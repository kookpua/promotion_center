using Microsoft.EntityFrameworkCore;
using PromotionCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionCenter.Context
{
    public class PromotionDbContext:DbContext
    {
        public PromotionDbContext(DbContextOptions options) : base(options)
        {
        }

        DbSet<Promotion> Promotions { get; set; }
        DbSet<PromotionProduct> PromotionProducts { get; set; }
    }
}
