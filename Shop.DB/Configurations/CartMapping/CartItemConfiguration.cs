using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.DB.Entities.Carts;

namespace Shop.DB.Configurations.CartMapping
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItems");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
                .HasColumnName("Id")
                .UseIdentityByDefaultColumn();

            builder.Property(ci => ci.CartId)
                .HasColumnName("CartId")
                .IsRequired();

            builder.Property(ci => ci.ProductVariantId)
                .HasColumnName("ProductVariantId")
                .IsRequired();

            builder.Property(ci => ci.Quantity)
                .HasColumnName("Quantity")
                .IsRequired();

            builder.ToTable("CartItems", t => t.HasCheckConstraint("CK_CartItem_Quantity", "\"Quantity\" > 0"));

            builder.HasIndex(ci => new { ci.CartId, ci.ProductVariantId })
                .IsUnique()
                .HasDatabaseName("IX_CartItems_CartId_ProductVariantId");

            builder.HasIndex(ci => ci.ProductVariantId)
                .HasDatabaseName("IX_CartItems_ProductVariantId");

            builder.HasOne(ci => ci.Cart)
                .WithMany(c => c.Items)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_CartItems_Carts_CartId");

            builder.HasOne(ci => ci.ProductVariant)
                .WithMany(pv => pv.CartItems)
                .HasForeignKey(ci => ci.ProductVariantId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_CartItems_ProductVariants_ProductVariantId");
        }
    }
}
