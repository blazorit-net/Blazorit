using Blazorit.Domain.Entities.EShop.EF.dom;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blazorit.Infrastructure.Configurations.EShop.EF.Dom;

public class DlyDeliveryMethodConfiguration : IEntityTypeConfiguration<DlyDeliveryMethod>
{
    public void Configure(EntityTypeBuilder<DlyDeliveryMethod> builder)
    {
        builder.Property(e => e.Method)
            .HasMaxLength(256);
    }
}