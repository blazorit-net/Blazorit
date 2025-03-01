using Blazorit.Domain.EShop.EF.dom;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blazorit.Infrastructure.Configurations.EShop.EF.Dom;

public class VwProdProductConfiguration: IEntityTypeConfiguration<VwProdProduct>
{
    public void Configure(EntityTypeBuilder<VwProdProduct> builder)
    {
        builder.HasNoKey();
        
        builder.Property(e => e.Category)
            .HasMaxLength(100);

        builder.Property(e => e.CategoryFullName)
            .HasMaxLength(200);

        builder.Property(e => e.Curr)
            .HasMaxLength(3);

        builder.Property(e => e.DateCreate);

        builder.Property(e => e.DateModified);

        builder.Property(e => e.DateTimeCreate);

        builder.Property(e => e.DateTimeModified);

        builder.Property(e => e.Description);

        builder.Property(e => e.Id);

        builder.Property(e => e.IsOnSite);

        builder.Property(e => e.LinkPart)
            .HasMaxLength(200);

        builder.Property(e => e.Name)
            .HasMaxLength(200);

        builder.Property(e => e.Price)
            .HasPrecision(16, 4);

        builder.Property(e => e.Sku)
            .HasMaxLength(50);
    }
}