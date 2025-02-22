using Blazorit.Domain.EShop.EF.dom;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blazorit.Infrastructure.Configurations.EShop.EF.Dom;

public class OrdOrderConfiguration : IEntityTypeConfiguration<OrdOrder>
{
    public void Configure(EntityTypeBuilder<OrdOrder> builder)
    {
        builder.HasIndex(e => e.DeliveryId).IsUnique();

        builder.HasIndex(e => e.PaymentId).IsUnique();

        builder.HasIndex(e => e.DeliveryId);

        builder.HasIndex(e => e.DeliveryId);

        builder.HasIndex(e => e.PaymentId);

        builder.HasIndex(e => e.UserId);

        builder.Property(e => e.DateTimeCreate)
            .HasDefaultValueSql("now()");

        builder.Property(e => e.DeliveryId);

        builder.Property(e => e.PaymentId);

        builder.Property(e => e.UserId);

        builder.HasOne(d => d.Delivery).WithOne(p => p.OrdOrder)
            .HasForeignKey<OrdOrder>(d => d.DeliveryId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(d => d.Payment).WithOne(p => p.OrdOrder)
            .HasForeignKey<OrdOrder>(d => d.PaymentId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}