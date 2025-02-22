using Blazorit.Domain.EShop.EF.dom;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blazorit.Infrastructure.Configurations.EShop.EF.Dom;

public class OrdOrderListConfiguration : IEntityTypeConfiguration<OrdOrderList>
{
    public void Configure(EntityTypeBuilder<OrdOrderList> builder)
    {
        builder.HasKey(e => new { e.OrderId, e.ProductId });

        builder.HasIndex(e => e.OrderId);

        builder.HasIndex(e => e.ProductId);

        builder.Property(e => e.Price)
            .HasPrecision(16, 4);

        builder.Property(e => e.Quantity);

        builder.HasOne(d => d.Order).WithMany(p => p.OrdOrderLists)
            .HasForeignKey(d => d.OrderId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(d => d.Product).WithMany(p => p.OrdOrderLists)
            .HasForeignKey(d => d.ProductId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}