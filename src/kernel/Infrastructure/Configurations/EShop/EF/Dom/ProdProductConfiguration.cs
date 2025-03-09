using Blazorit.Domain.Entities.EShop.EF.dom;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blazorit.Infrastructure.Configurations.EShop.EF.Dom;

public class ProdProductConfiguration: IEntityTypeConfiguration<ProdProduct>
{
    public void Configure(EntityTypeBuilder<ProdProduct> builder)
    {
        builder.HasIndex(e => new { e.LinkPart, e.CategoryId }).IsUnique();

        builder.HasIndex(e => e.Sku).IsUnique();

        builder.HasIndex(e => e.CategoryId);

        builder.Property(e => e.Curr)
            .HasMaxLength(3);

        builder.Property(e => e.DateTimeCreate)
            .HasDefaultValueSql("now()");

        builder.Property(e => e.DateTimeModified)
            .HasDefaultValueSql("now()");

        builder.Property(e => e.IsOnSite)
            .IsRequired()
            .HasDefaultValueSql("true");

        builder.Property(e => e.LinkPart)
            .HasMaxLength(200)
            .HasDefaultValueSql("'empty'::character varying");

        builder.Property(e => e.Name)
            .HasMaxLength(200);

        builder.Property(e => e.Price)
            .HasPrecision(16, 4);

        builder.Property(e => e.Sku)
            .HasMaxLength(50);
    }
}