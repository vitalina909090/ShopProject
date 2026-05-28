using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.DB.Entities.Catalog.Products;

namespace Shop.DB.Configurations.Catalog.ProductMapping
{
    public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
    {
        public void Configure(EntityTypeBuilder<ProductVariant> builder)
        {
            builder.ToTable("ProductVariants");

            builder.HasKey(pv => pv.Id);

            builder.Property(pv => pv.Id)
                .HasColumnName("Id")
                .UseIdentityByDefaultColumn();

            builder.Property(pv => pv.ProductId)
                .HasColumnName("ProductId")
                .IsRequired();

            builder.Property(pv => pv.Price)
                .HasColumnName("Price")
                .HasColumnType("numeric(10,2)")
                .IsRequired();

            builder.Property(pv => pv.StockQuantity)
                .HasColumnName("StockQuantity")
                .IsRequired();

            builder.Property(pv => pv.VariantKey)
                .HasColumnName("VariantKey")
                .HasMaxLength(200)
                .IsRequired();

            builder.ToTable("ProductVariants", t =>
            {
                t.HasCheckConstraint("CK_ProductVariant_Price", "\"Price\" > 0");
                t.HasCheckConstraint("CK_ProductVariant_StockQuantity", "\"StockQuantity\" >= 0");
            });

            builder.HasIndex(pv => new { pv.ProductId, pv.VariantKey })
                .IsUnique()
                .HasDatabaseName("IX_ProductVariants_ProductId_VariantKey");

            builder.HasIndex(pv => pv.StockQuantity)
                .HasFilter("\"StockQuantity\" > 0")
                .HasDatabaseName("IX_ProductVariants_StockQuantity_InStock");

            builder.HasOne(pv => pv.Product)
                .WithMany(p => p.Variants)
                .HasForeignKey(pv => pv.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ProductVariants_Products_ProductId");
        }
    }
}
