using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.DB.Entities.Reviews;

namespace Shop.DB.Configurations.ReviewMapping
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Reviews");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .HasColumnName("Id")
                .UseIdentityByDefaultColumn();

            builder.Property(r => r.UserId)
                .HasColumnName("UserId")
                .IsRequired();

            builder.Property(r => r.ProductId)
                .HasColumnName("ProductId")
                .IsRequired();

            builder.Property(r => r.Rating)
                .HasColumnName("Rating")
                .IsRequired();

            builder.Property(r => r.Comment)
                .HasColumnName("Comment")
                .HasMaxLength(2000);

            builder.Property(r => r.IsApproved)
                .HasColumnName("IsApproved")
                .IsRequired();

            builder.Property(r => r.CreatedAt)
                .HasColumnName("CreatedAt")
                .IsRequired();

            builder.Property(r => r.OrderId)
                .HasColumnName("OrderId");

            builder.ToTable("Reviews", t => t.HasCheckConstraint("CK_Review_Rating", "\"Rating\" >= 1 AND \"Rating\" <= 5"));

            builder.HasIndex(r => new { r.UserId, r.ProductId })
                .IsUnique()
                .HasDatabaseName("IX_Reviews_UserId_ProductId");

            builder.HasIndex(r => r.ProductId)
                .HasDatabaseName("IX_Reviews_ProductId");

            builder.HasIndex(r => r.OrderId)
                .HasDatabaseName("IX_Reviews_OrderId");

            builder.HasIndex(r => new { r.ProductId, r.IsApproved, r.CreatedAt })
                .HasDatabaseName("IX_Reviews_ProductId_IsApproved_CreatedAt");

            builder.HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Reviews_Users_UserId");

            builder.HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Reviews_Products_ProductId");

            builder.HasOne(r => r.Order)
                .WithMany(o => o.Reviews)
                .HasForeignKey(r => r.OrderId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_Reviews_Orders_OrderId");
        }
    }
}
