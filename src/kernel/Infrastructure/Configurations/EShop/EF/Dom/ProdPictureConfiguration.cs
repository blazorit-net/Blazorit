using Blazorit.Domain.Entities.EShop.EF.dom;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blazorit.Infrastructure.Configurations.EShop.EF.Dom;

public class ProdPictureConfiguration : IEntityTypeConfiguration<ProdPicture>
{
    public void Configure(EntityTypeBuilder<ProdPicture> builder)
    {
        builder.HasIndex(e => new { e.ProductId, e.PicSize, e.SiteLocation });

        builder.HasIndex(e => new { e.ProductId, e.PicSize, e.SiteLocation, e.OrderNum }).IsUnique();

        builder.Property(e => e.LinkPart)
            .HasMaxLength(100);

        builder.Property(e => e.OrderNum);

        builder.Property(e => e.PicSize)
            .HasMaxLength(10)
            .HasDefaultValueSql("'medium'::character varying");

        builder.Property(e => e.SiteLocation)
            .HasMaxLength(50)
            .HasDefaultValueSql("'site'::character varying");

        builder.HasOne(d => d.Product).WithMany(p => p.ProdPictures)
            .HasForeignKey(d => d.ProductId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}