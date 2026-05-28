using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.DB.Entities.Catalog.Products.Options;

namespace Shop.DB.Configurations.Catalog.ProductMapping.OptionMapping
{
    public class ProductOptionValueConfiguration : IEntityTypeConfiguration<ProductOptionValue>
    {
        public void Configure(EntityTypeBuilder<ProductOptionValue> builder)
        {
            builder.ToTable("ProductOptionValues");

            builder.HasKey(pov => pov.Id);

            builder.Property(pov => pov.Id)
                .HasColumnName("Id")
                .UseIdentityByDefaultColumn();

            builder.Property(pov => pov.ProductOptionId)
                .HasColumnName("ProductOptionId")
                .IsRequired();

            builder.Property(pov => pov.Value)
                .HasColumnName("Value")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(pov => pov.SortOrder)
                .HasColumnName("SortOrder")
                .IsRequired();

            builder.Property(pov => pov.ColorHex)
                .HasColumnName("ColorHex")
                .HasMaxLength(7);

            builder.HasIndex(pov => new { pov.ProductOptionId, pov.Value })
                .IsUnique()
                .HasDatabaseName("IX_ProductOptionValues_ProductOptionId_Value");

            builder.HasOne(pov => pov.ProductOption)
                .WithMany(po => po.Values)
                .HasForeignKey(pov => pov.ProductOptionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ProductOptionValues_ProductOptions_ProductOptionId");
        }
    }
}
