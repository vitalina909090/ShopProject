using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.DB.Entities.Catalog.Products;

namespace Shop.DB.Configurations.Catalog.ProductMapping
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("Id")
                .UseIdentityByDefaultColumn();

            builder.Property(p => p.Name)
                .HasColumnName("Name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasColumnName("Description")
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(p => p.IsArchived)
                .HasColumnName("IsArchived")
                .IsRequired();

            builder.Property(p => p.IsNew)
                .HasColumnName("IsNew")
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(p => p.IsPopular)
                .HasColumnName("IsPopular")
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(p => p.CatalogVariantId)
                .HasColumnName("CatalogVariantId");

            builder.Property(p => p.CategoryId)
                .HasColumnName("CategoryId")
                .IsRequired();

            builder.Property(p => p.CreatedAt)
                .HasColumnName("CreatedAt")
                .IsRequired();

            builder.Property(p => p.UpdatedAt)
                .HasColumnName("UpdatedAt")
                .IsRequired();

            builder.HasIndex(p => p.Name)
                .IsUnique()
                .HasDatabaseName("IX_Products_Name");

            builder.HasIndex(p => p.CategoryId)
                .HasDatabaseName("IX_Products_CategoryId");

            builder.HasIndex(p => p.IsNew)
                .HasDatabaseName("IX_Products_IsNew");

            builder.HasIndex(p => p.IsPopular)
                .HasDatabaseName("IX_Products_IsPopular");

            builder.HasIndex(p => p.CatalogVariantId)
                .HasDatabaseName("IX_Products_CatalogVariantId");

            builder.HasIndex(p => new { p.IsArchived, p.CategoryId })
                .HasDatabaseName("IX_Products_IsArchived_CategoryId");

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Products_Categories_CategoryId");

            builder.HasOne(p => p.CatalogVariant)
                .WithMany(pv => pv.CatalogForProducts)
                .HasForeignKey(p => p.CatalogVariantId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Products_ProductVariants_CatalogVariantId");
        }
    }
}
