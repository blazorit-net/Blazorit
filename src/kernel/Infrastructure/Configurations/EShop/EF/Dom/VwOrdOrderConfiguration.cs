using Blazorit.Domain.EShop.EF.dom;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blazorit.Infrastructure.Configurations.EShop.EF.Dom;

public class VwOrdOrderConfiguration: IEntityTypeConfiguration<VwOrdOrder>
{
    public void Configure(EntityTypeBuilder<VwOrdOrder> builder)
    {
        builder.HasNoKey();
        
        builder.Property(e => e.Category)
            .HasMaxLength(100);

        builder.Property(e => e.Curr)
            .HasMaxLength(3);

        builder.Property(e => e.DateCreate);

        builder.Property(e => e.DateTimeCreate);

        builder.Property(e => e.DeliveryId);

        builder.Property(e => e.Name)
            .HasMaxLength(200);

        builder.Property(e => e.OrderId);

        builder.Property(e => e.OrderPrice)
            .HasPrecision(16, 4);

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