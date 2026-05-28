using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.DB.Entities.Discounts;


namespace Shop.DB.Configurations.DiscountMapping
{
    public class DiscountProductConfiguration : IEntityTypeConfiguration<DiscountProduct>
    {
        public void Configure(EntityTypeBuilder<DiscountProduct> builder)
        {
            builder.ToTable("DiscountProducts");

            builder.HasKey(dp => new { dp.DiscountId, dp.ProductId });

            builder.Property(dp => dp.DiscountId)
                .HasColumnName("DiscountId");

            builder.Property(dp => dp.ProductId)
                .HasColumnName("ProductId");

            builder.HasIndex(dp => dp.ProductId)
                .HasDatabaseName("IX_DiscountProducts_ProductId");

            builder.HasOne(dp => dp.Discount)
                .WithMany(d => d.DiscountProducts)
                .HasForeignKey(dp => dp.DiscountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_DiscountProducts_Discounts_DiscountId");

            builder.HasOne(dp => dp.Product)
                .WithMany(p => p.DiscountProducts)
                .HasForeignKey(dp => dp.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_DiscountProducts_Products_ProductId");
        }
    }
}
