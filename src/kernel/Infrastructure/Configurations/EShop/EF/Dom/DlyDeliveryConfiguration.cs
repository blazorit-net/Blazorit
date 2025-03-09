using Blazorit.Domain.Entities.EShop.EF.dom;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blazorit.Infrastructure.Configurations.EShop.EF.Dom;

public class DlyDeliveryConfiguration : IEntityTypeConfiguration<DlyDelivery>
{
    public void Configure(EntityTypeBuilder<DlyDelivery> builder)
    {
        builder.Property(e => e.DateTimeCreate)
            .HasDefaultValueSql("now()");

        builder.Property(e => e.DeliveryCost)
            .HasPrecision(16, 4);

        builder.Property(e => e.DeliveryTimeEnd)
            .HasColumnType("time with time zone");

        builder.Property(e => e.DeliveryTimeStart)
            .HasColumnType("time with time zone");

        builder.HasOne(d => d.UserDelivery).WithMany(p => p.DlyDeliveries)
            .HasForeignKey(d => d.UserDeliveryId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}