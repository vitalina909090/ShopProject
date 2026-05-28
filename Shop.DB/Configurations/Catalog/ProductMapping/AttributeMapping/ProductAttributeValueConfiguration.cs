using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.DB.Entities.Catalog.Products.Attributes;

namespace Shop.DB.Configurations.Catalog.ProductMapping.AttributeMapping
{
    public class ProductAttributeValueConfiguration : IEntityTypeConfiguration<ProductAttributeValue>
    {
        public void Configure(EntityTypeBuilder<ProductAttributeValue> builder)
        {
            builder.ToTable("ProductAttributeValues");

            builder.HasKey(pav => pav.Id);

            builder.Property(pav => pav.Id)
                .HasColumnName("Id")
                .UseIdentityByDefaultColumn();

            builder.Property(pav => pav.ProductAttributeId)
                .HasColumnName("ProductAttributeId")
                .IsRequired();

            builder.Property(pav => pav.Value)
                .HasColumnName("Value")
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(pav => new { pav.ProductAttributeId, pav.Value })
                .IsUnique()
                .HasDatabaseName("IX_ProductAttributeValues_ProductAttributeId_Value");

            builder.HasOne(pav => pav.ProductAttribute)
                .WithMany(pa => pa.Values)
                .HasForeignKey(pav => pav.ProductAttributeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ProductAttributeValues_ProductAttributes_ProductAttributeId");
        }
    }
}
