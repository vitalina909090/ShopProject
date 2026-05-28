using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.DB.Entities.Catalog.Products.Attributes;

namespace Shop.DB.Configurations.Catalog.ProductMapping.AttributeMapping
{
    public class ProductAttributeConfiguration : IEntityTypeConfiguration<ProductAttribute>
    {
        public void Configure(EntityTypeBuilder<ProductAttribute> builder)
        {
            builder.ToTable("ProductAttributes");

            builder.HasKey(pa => pa.Id);

            builder.Property(pa => pa.Id)
                .HasColumnName("Id")
                .UseIdentityByDefaultColumn();

            builder.Property(pa => pa.Name)
                .HasColumnName("Name")
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(pa => pa.Name)
                .IsUnique()
                .HasDatabaseName("IX_ProductAttributes_Name");
        }
    }
}
