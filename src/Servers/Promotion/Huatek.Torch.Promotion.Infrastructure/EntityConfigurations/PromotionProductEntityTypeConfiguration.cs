using Huatek.Torch.Promotion.Domain.PromotionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Huatek.Torch.Promotion.Infrastructure.EntityConfigurations
{
    class PromotionProductEntityTypeConfiguration : IEntityTypeConfiguration<PromotionProduct>
    {
        public void Configure(EntityTypeBuilder<PromotionProduct> builder)
        {
            builder.HasKey(p => p.Id);
            builder.ToTable("PromotionProduct");
            builder.Property(p => p.Price).HasColumnType("decimal(18,4)");
           
        }
    }
}
