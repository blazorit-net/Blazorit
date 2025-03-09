using Blazorit.Domain.Entities.EShop.EF.dom;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blazorit.Infrastructure.Configurations.EShop.EF.Dom;

public class CartShopcartListConfiguration: IEntityTypeConfiguration<CartShopcartList>
{
    public void Configure(EntityTypeBuilder<CartShopcartList> builder)
    {
        builder.HasKey(e => new { e.CartId, e.ProductId });

        builder.Property(e => e.DateTimeCreated)
            .HasDefaultValueSql("now()");

        builder.HasOne(d => d.Cart).WithMany(p => p.CartShopcartLists)
            .HasForeignKey(d => d.CartId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(d => d.Product).WithMany(p => p.CartShopcartLists)
            .HasForeignKey(d => d.ProductId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}