using Blazorit.Domain.Entities.EShop.EF.dom;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blazorit.Infrastructure.Configurations.EShop.EF.Dom;

public class WishWishConfiguration: IEntityTypeConfiguration<WishWish>
{
    public void Configure(EntityTypeBuilder<WishWish> builder)
    {
        builder.HasIndex(e => e.UserId).IsUnique();

        builder.Property(e => e.DateTimeCreate)
            .HasDefaultValueSql("now()");
    }
}