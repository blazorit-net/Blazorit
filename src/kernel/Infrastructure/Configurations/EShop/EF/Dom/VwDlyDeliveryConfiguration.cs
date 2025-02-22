using Blazorit.Domain.EShop.EF.dom;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blazorit.Infrastructure.Configurations.EShop.EF.Dom;

public class VwDlyDeliveryConfiguration: IEntityTypeConfiguration<VwDlyDelivery>
{
    public void Configure(EntityTypeBuilder<VwDlyDelivery> builder)
    {
        builder.Property(e => e.Address)
            .HasMaxLength(200);

        builder.Property(e => e.AddressId);

        builder.Property(e => e.Comment)
            .HasMaxLength(300);

        builder.Property(e => e.DateTimeCreate);

        builder.Property(e => e.DeliveryCost)
            .HasPrecision(16, 4);

        builder.Property(e => e.DeliveryDate);

        builder.Property(e => e.DeliveryTimeEnd)
            .HasColumnType("time with time zone");

        builder.Property(e => e.DeliveryTimeStart)
            .HasColumnType("time with time zone");

        builder.Property(e => e.Id);

        builder.Property(e => e.Method)
            .HasMaxLength(256);

        builder.Property(e => e.MethodId);

        builder.Property(e => e.UserId);
    }
}