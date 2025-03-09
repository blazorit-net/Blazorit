using Blazorit.Domain.Entities.EShop.EF.dom;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blazorit.Infrastructure.Configurations.EShop.EF.Dom;

public class WishWishListConfiguration: IEntityTypeConfiguration<WishWishList>
{
    public void Configure(EntityTypeBuilder<WishWishList> builder)
    {
        builder.Property(e => e.DateTimeCreate)
            .HasDefaultValueSql("now()");

        builder.HasOne(d => d.Product).WithMany(p => p.WishWishLists)
            .HasForeignKey(d => d.ProductId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(d => d.Wish).WithMany(p => p.WishWishLists)
            .HasForeignKey(d => d.WishId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}