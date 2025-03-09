using Blazorit.Domain.Entities.EShop.EF.dom;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blazorit.Infrastructure.Configurations.EShop.EF.Dom;

public class PmntPaymentConfiguration : IEntityTypeConfiguration<PmntPayment>
{
    public void Configure(EntityTypeBuilder<PmntPayment> builder)
    {
        builder.HasIndex(e => e.CheckoutOrderId).IsUnique();

        builder.HasIndex(e => e.PaymentMethodId);

        builder.HasIndex(e => e.CheckoutOrderId);

        builder.Property(e => e.DateTimeCreate)
            .HasDefaultValueSql("now()");

        builder.Property(e => e.IsPaid);

        builder.Property(e => e.OrderToken)
            .HasMaxLength(100);

        builder.Property(e => e.PaymentAmount)
            .HasPrecision(16, 4);

        builder.Property(e => e.PaymentInfo);

        builder.Property(e => e.PaymentMethodId);

        builder.HasOne(d => d.PaymentMethod).WithMany(p => p.PmntPayments)
            .HasForeignKey(d => d.PaymentMethodId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}