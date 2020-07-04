using Huatek.Torch.Promotion.Domain.PromotionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Huatek.Torch.Promotion.Infrastructure.EntityConfigurations
{
    class PromotionEntityTypeConfiguration : IEntityTypeConfiguration<Promotions>
    {
        public void Configure(EntityTypeBuilder<Promotions> builder)
        {
            builder.HasKey(p => p.Id);
            builder.ToTable("Promotion");
            builder.Property(p => p.Title).IsRequired(true).HasMaxLength(50);
            builder.Property(p => p.Description).IsRequired(true).HasMaxLength(1000);
           
        }
    }
}
