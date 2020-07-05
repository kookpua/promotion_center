using Huatek.Torch.Promotions.Domain.PromotionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Huatek.Torch.Promotions.Infrastructure.EntityConfigurations
{
    class PromotionEntityTypeConfiguration : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.ToTable("Promotion");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Title).IsRequired(true).HasMaxLength(50);
            builder.Property(p => p.Description).IsRequired(true).HasMaxLength(1000);
           
        }
    }
}
