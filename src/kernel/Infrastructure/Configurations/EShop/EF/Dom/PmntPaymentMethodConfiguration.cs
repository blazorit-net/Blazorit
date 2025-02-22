using Blazorit.Domain.EShop.EF.dom;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blazorit.Infrastructure.Configurations.EShop.EF.Dom;

public class PmntPaymentMethodConfiguration : IEntityTypeConfiguration<PmntPaymentMethod>
{
    public void Configure(EntityTypeBuilder<PmntPaymentMethod> builder)
    {
        builder.Property(e => e.IsCod);

        builder.Property(e => e.Method)
            .HasMaxLength(200);

        builder.Property(e => e.Ordby);
    }
}