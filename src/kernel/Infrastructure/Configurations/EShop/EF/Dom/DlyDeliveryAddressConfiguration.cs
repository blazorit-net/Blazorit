using Blazorit.Domain.Entities.EShop.EF.dom;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blazorit.Infrastructure.Configurations.EShop.EF.Dom;

public class DlyDeliveryAddressConfiguration : IEntityTypeConfiguration<DlyDeliveryAddress>
{
    public void Configure(EntityTypeBuilder<DlyDeliveryAddress> builder)
    {
        builder.Property(e => e.Address)
            .HasMaxLength(200);

        builder.Property(e => e.Comment)
            .HasMaxLength(300);

        builder.Property(e => e.DateTimeCreated)
            .HasDefaultValueSql("now()");
    }
}