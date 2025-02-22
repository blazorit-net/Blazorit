using Blazorit.Domain.EShop.EF.dom;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blazorit.Infrastructure.Configurations.EShop.EF.Dom;

public class DlyUserDeliveryConfiguration: IEntityTypeConfiguration<DlyUserDelivery>
{
    public void Configure(EntityTypeBuilder<DlyUserDelivery> builder)
    {
        builder.HasIndex(e => new { e.UserId, e.MethodId, e.AddressId }).IsUnique();

        builder.HasIndex(e => e.AddressId);
        builder.HasIndex(e => e.MethodId);
        builder.HasIndex(e => e.UserId);

        builder.Property(e => e.DateTimeCreated)
            .HasDefaultValueSql("now()");
    }
}