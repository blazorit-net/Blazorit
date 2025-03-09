using Blazorit.Domain.Entities.EShop.EF.dom;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blazorit.Infrastructure.Configurations.EShop.EF.Dom;

public class VwCartShopcartConfiguration: IEntityTypeConfiguration<VwCartShopcart>
{
    public void Configure(EntityTypeBuilder<VwCartShopcart> builder)
    {
        builder.HasNoKey();
        
        builder.Property(e => e.CartId);

        builder.Property(e => e.Category)
            .HasMaxLength(100);

        builder.Property(e => e.Curr)
            .HasMaxLength(3);

        builder.Property(e => e.DateCreate);

        builder.Property(e => e.DateTimeCreate);

        builder.Property(e => e.DateTimeItemCreate);

        builder.Property(e => e.Name)
            .HasMaxLength(200);

        builder.Property(e => e.ProductId);

        builder.Property(e => e.ProductLinkPart)
            .HasMaxLength(200);

        builder.Property(e => e.ProductPrice)
            .HasPrecision(16, 4);

        builder.Property(e => e.Quantity);

        builder.Property(e => e.Sku)
            .HasMaxLength(50);

        builder.Property(e => e.UserId);
    }
}