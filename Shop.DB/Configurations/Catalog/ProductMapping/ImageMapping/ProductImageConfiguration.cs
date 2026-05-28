using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.DB.Entities.Catalog.Products.Images;

namespace Shop.DB.Configurations.Catalog.ProductMapping.ImageMapping
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages");

            builder.HasKey(pi => pi.Id);

            builder.Property(pi => pi.Id)
                .HasColumnName("Id")
                .UseIdentityByDefaultColumn();

            builder.Property(pi => pi.ProductId)
                .HasColumnName("ProductId")
                .IsRequired();

            builder.Property(pi => pi.ProductOptionValueId)
                .HasColumnName("ProductOptionValueId");

            builder.Property(pi => pi.Url)
                .HasColumnName("Url")
                .IsRequired();

            builder.Property(pi => pi.SortOrder)
                .HasColumnName("SortOrder")
                .IsRequired();

            builder.ToTable("ProductImages", t => t.HasCheckConstraint("CK_ProductImage_SortOrder", "\"SortOrder\" >= 0"));

            builder.HasIndex(pi => new { pi.ProductId, pi.ProductOptionValueId, pi.SortOrder })
                .IsUnique()
                .HasDatabaseName("IX_ProductImages_ProductId_ProductOptionValueId_SortOrder");
 
            builder.HasIndex(pi => new { pi.ProductId, pi.SortOrder })
                .IsUnique()
                .HasFilter("\"ProductOptionValueId\" IS NULL")
                .HasDatabaseName("IX_ProductImages_ProductId_SortOrder");

            builder.HasIndex(pi => new { pi.ProductId, pi.Url })
                .IsUnique()
                .HasDatabaseName("IX_ProductImages_ProductId_Url");

            builder.HasIndex(pi => pi.ProductOptionValueId)
                .HasDatabaseName("IX_ProductImages_ProductOptionValueId");

            builder.HasOne(pi => pi.Product)
                .WithMany(p => p.Images)
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ProductImages_Products_ProductId");

            builder.HasOne(pi => pi.ProductOptionValue)
                .WithMany(pov => pov.Images)
                .HasForeignKey(pi => pi.ProductOptionValueId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_ProductImages_ProductOptionValues_ProductOptionValueId");
        }
    }
}
