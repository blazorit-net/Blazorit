using Blazorit.Domain.EShop.EF.dom;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blazorit.Infrastructure.Configurations.EShop.EF.Dom;

public class OrdCheckoutOrderConfiguration: IEntityTypeConfiguration<OrdCheckoutOrder>
{
    public void Configure(EntityTypeBuilder<OrdCheckoutOrder> builder)
    {
        builder.HasIndex(e => e.OrderToken).IsUnique();
        builder.HasIndex(e => new { e.OrderToken, e.Canceled }).IsUnique();

        builder.Property(e => e.Canceled)
            .IsRequired()
            .HasDefaultValueSql("true");

        builder.Property(e => e.DateTimeCreated)
            .HasDefaultValueSql("now()");

        builder.Property(e => e.DeliveryId);
        builder.Property(e => e.OrderToken)
            .HasMaxLength(100);

        builder.Property(e => e.PaymentAmount)
            .HasPrecision(16, 4);

        builder.Property(e => e.PaymentMethodId);
        builder.Property(e => e.UserId);
    }
}