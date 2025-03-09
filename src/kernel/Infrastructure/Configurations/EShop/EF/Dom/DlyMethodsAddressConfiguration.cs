using Blazorit.Domain.Entities.EShop.EF.dom;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blazorit.Infrastructure.Configurations.EShop.EF.Dom;

public class DlyMethodsAddressConfiguration: IEntityTypeConfiguration<DlyMethodsAddress>
{
    public void Configure(EntityTypeBuilder<DlyMethodsAddress> builder)
    {
        builder.HasIndex(e => new { e.MethodId, e.AddressId }).IsUnique();

        builder.HasIndex(e => e.AddressId);

        builder.Property(e => e.AddressId);
        builder.Property(e => e.MethodId);
    }
}