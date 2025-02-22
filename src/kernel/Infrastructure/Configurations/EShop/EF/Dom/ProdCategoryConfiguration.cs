using Blazorit.Domain.EShop.EF.dom;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blazorit.Infrastructure.Configurations.EShop.EF.Dom;

public class ProdCategoryConfiguration : IEntityTypeConfiguration<ProdCategory>
{
    public void Configure(EntityTypeBuilder<ProdCategory> builder)
    {
        builder.HasIndex(e => e.Name).IsUnique();

        builder.Property(e => e.FullName)
            .HasMaxLength(200);

        builder.Property(e => e.Name)
            .HasMaxLength(100);

        builder.Property(e => e.PrefixSku)
            .HasMaxLength(20);
    }
}