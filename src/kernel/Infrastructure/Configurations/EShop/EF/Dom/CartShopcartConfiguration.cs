using Blazorit.Domain.Entities.EShop.EF.dom;
using Blazorit.Domain.Entities.EShop.EF.dom;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blazorit.Infrastructure.Configurations.EShop.EF.Dom;

public class CartShopcartConfiguration: IEntityTypeConfiguration<CartShopcart>
{
    public void Configure(EntityTypeBuilder<CartShopcart> builder)
    {
        builder.HasIndex(e => e.UserId).IsUnique();

        builder.HasIndex(e => e.UserId);

        builder.Property(e => e.DateTimeCreate)
            .HasDefaultValueSql("now()");
    }
}