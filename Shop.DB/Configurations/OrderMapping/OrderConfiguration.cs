using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.DB.Entities.Orders;

namespace Shop.DB.Configurations.OrderMapping
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .HasColumnName("Id")
                .UseIdentityByDefaultColumn();

            builder.Property(o => o.UserId)
                .HasColumnName("UserId");

            builder.Property(o => o.TotalAmount)
                .HasColumnName("TotalAmount")
                .HasColumnType("numeric(10,2)")
                .IsRequired();

            builder.Property(o => o.Status)
                .HasColumnName("Status")
                .IsRequired();

            builder.Property(o => o.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            builder.Property(o => o.CustomerEmail)
                .HasColumnName("CustomerEmail")
                .HasMaxLength(100)
                .HasDefaultValue("")
                .IsRequired();

            builder.Property(o => o.CustomerName)
                .HasColumnName("CustomerName")
                .HasMaxLength(100)
                .HasDefaultValue("")
                .IsRequired();

            builder.Property(o => o.CustomerPhone)
                .HasColumnName("CustomerPhone")
                .HasMaxLength(20);

            builder.Property(o => o.ShippingAddress)
                .HasColumnName("ShippingAddress")
                .HasMaxLength(300)
                .HasDefaultValue("")
                .IsRequired();

            builder.Property(o => o.StripePaymentIntentId)
                .HasColumnName("StripePaymentIntentId");

            builder.Property(o => o.PostalCode)
                .HasColumnName("PostalCode")
                .HasMaxLength(20);

            builder.ToTable("Orders", t => t.HasCheckConstraint("CK_Order_TotalAmount", "\"TotalAmount\" >= 0"));

            builder.HasIndex(o => o.UserId)
                .HasDatabaseName("IX_Orders_UserId");

            builder.HasIndex(o => new { o.Status, o.CreatedAt })
                .HasDatabaseName("IX_Orders_Status_CreatedAt");

            builder.HasIndex(o => o.StripePaymentIntentId)
                .HasFilter("\"StripePaymentIntentId\" IS NOT NULL")
                .HasDatabaseName("IX_Orders_StripePaymentIntentId");

            builder.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Orders_Users_UserId");
        }
    }
}
