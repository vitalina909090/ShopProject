using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.DB.Entities.Wishlists;

namespace Shop.DB.Configurations.WishlistMapping
{
    public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {
            builder.ToTable("Wishlists");

            builder.HasKey(w => new { w.UserId, w.ProductId });

            builder.Property(w => w.UserId)
                .HasColumnName("UserId");

            builder.Property(w => w.ProductId)
                .HasColumnName("ProductId");

            builder.HasIndex(w => w.ProductId)
                .HasDatabaseName("IX_Wishlists_ProductId");

            builder.HasIndex(w => new { w.UserId, w.ProductId })
                .IsUnique()
                .HasDatabaseName("IX_Wishlists_UserId_ProductId");

            builder.HasOne(w => w.User)
                .WithMany(u => u.Wishlists)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Wishlists_Users_UserId");

            builder.HasOne(w => w.Product)
                .WithMany(p => p.Wishlists)
                .HasForeignKey(w => w.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Wishlists_Products_ProductId");
        }
    }
}
