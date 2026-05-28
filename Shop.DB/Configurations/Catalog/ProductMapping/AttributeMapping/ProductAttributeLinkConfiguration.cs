using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.DB.Entities.Catalog.Products.Attributes;

namespace Shop.DB.Configurations.Catalog.ProductMapping.AttributeMapping
{
    public class ProductAttributeLinkConfiguration : IEntityTypeConfiguration<ProductAttributeLink>
    {
        public void Configure(EntityTypeBuilder<ProductAttributeLink> builder)
        {
            builder.ToTable("ProductAttributeLinks");

            builder.HasKey(pal => new { pal.ProductId, pal.ProductAttributeValueId });

            builder.Property(pal => pal.ProductId)
                .HasColumnName("ProductId");

            builder.Property(pal => pal.ProductAttributeValueId)
                .HasColumnName("ProductAttributeValueId");

            builder.HasIndex(pal => pal.ProductAttributeValueId)
                .HasDatabaseName("IX_ProductAttributeLinks_ProductAttributeValueId");

            builder.HasOne(pal => pal.Product)
                .WithMany(p => p.AttributeLinks)
                .HasForeignKey(pal => pal.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ProductAttributeLinks_Products_ProductId");

            builder.HasOne(pal => pal.ProductAttributeValue)
                .WithMany(pav => pav.AttributeLinks)
                .HasForeignKey(pal => pal.ProductAttributeValueId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ProductAttributeLinks_ProductAttributeValues_ProductAttribu~");
        }
    }
}
