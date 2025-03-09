using Blazorit.Domain.Entities.EShop.EF.dom;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blazorit.Infrastructure.Configurations.EShop.EF.Dom;

public class VwDlyMethodsAddressConfiguration: IEntityTypeConfiguration<VwDlyMethodsAddress>
{
    public void Configure(EntityTypeBuilder<VwDlyMethodsAddress> builder)
    {
        builder.HasNoKey();
        
        builder.Property(e => e.Address)
            .HasMaxLength(200);

        builder.Property(e => e.AddressId);

        builder.Property(e => e.Comment)
            .HasMaxLength(300);

        builder.Property(e => e.Id);

        builder.Property(e => e.Method)
            .HasMaxLength(256);

        builder.Property(e => e.MethodId);
    }
}