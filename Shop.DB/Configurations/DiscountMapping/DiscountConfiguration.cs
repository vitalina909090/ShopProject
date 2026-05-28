using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.DB.Entities.Discounts;

namespace Shop.DB.Configurations.DiscountMapping
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.ToTable("Discounts");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id)
                .HasColumnName("Id")
                .UseIdentityByDefaultColumn();

            builder.Property(d => d.Name)
                .HasColumnName("Name")
                .IsRequired();

            builder.Property(d => d.TypeId)
                .HasColumnName("TypeId")
                .IsRequired();

            builder.Property(d => d.Form)
                .HasColumnName("Form")
                .IsRequired();

            builder.Property(d => d.Value)
                .HasColumnName("Value")
                .HasColumnType("numeric")
                .IsRequired();

            builder.Property(d => d.StartAt)
                .HasColumnName("StartAt");

            builder.Property(d => d.EndAt)
                .HasColumnName("EndAt");

            builder.Property(d => d.IsActive)
                .HasColumnName("IsActive")
                .IsRequired();

            builder.Property(d => d.ProductId)
                .HasColumnName("ProductId");

            builder.Property(d => d.ProductVariantId)
                .HasColumnName("ProductVariantId");

            builder.Property(d => d.CategoryId)
                .HasColumnName("CategoryId");

            builder.ToTable("Discounts", t =>
            {
                t.HasCheckConstraint("CK_Discount_Date", "(\"StartAt\" IS NULL) OR (\"EndAt\" IS NULL) OR (\"StartAt\" <= \"EndAt\")");
                t.HasCheckConstraint("CK_Discount_Value", "\"Value\" > 0");
            });

            builder.HasIndex(d => d.Name)
                .IsUnique()
                .HasDatabaseName("IX_Discounts_Name");

            builder.HasIndex(d => d.TypeId)
                .HasDatabaseName("IX_Discounts_TypeId");

            builder.HasIndex(d => d.ProductId)
                .HasDatabaseName("IX_Discounts_ProductId");

            builder.HasIndex(d => d.ProductVariantId)
                .HasDatabaseName("IX_Discounts_ProductVariantId");

            builder.HasIndex(d => d.CategoryId)
                .HasDatabaseName("IX_Discounts_CategoryId");

            builder.HasIndex(d => new { d.IsActive, d.StartAt, d.EndAt })
                .HasDatabaseName("IX_Discounts_IsActive_StartAt_EndAt");

            builder.HasOne(d => d.DiscountType)
                .WithMany(dt => dt.Discounts)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Discounts_DiscountTypes_TypeId");

            builder.HasOne(d => d.Product)
                .WithMany(p => p.Discounts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Discounts_Products_ProductId");

            builder.HasOne(d => d.ProductVariant)
                .WithMany(pv => pv.Discounts)
                .HasForeignKey(d => d.ProductVariantId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Discounts_ProductVariants_ProductVariantId");

            builder.HasOne(d => d.Category)
                .WithMany(c => c.Discounts)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Discounts_Categories_CategoryId");
        }
    }
}
