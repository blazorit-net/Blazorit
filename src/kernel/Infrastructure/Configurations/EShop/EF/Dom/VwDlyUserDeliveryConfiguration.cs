using Blazorit.Domain.Entities.EShop.EF.dom;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blazorit.Infrastructure.Configurations.EShop.EF.Dom;

public class VwDlyUserDeliveryConfiguration: IEntityTypeConfiguration<VwDlyUserDelivery>
{
    public void Configure(EntityTypeBuilder<VwDlyUserDelivery> builder)
    {
        builder.HasNoKey();
        
        builder.Property(e => e.Address)
            .HasMaxLength(200);

        builder.Property(e => e.Comment)
            .HasMaxLength(300);

        builder.Property(e => e.DateTimeCreated);

        builder.Property(e => e.Id);

        builder.Property(e => e.Method)
            .HasMaxLength(256);

        builder.Property(e => e.UserId);
    }
}