using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.DB.Entities.Orders;

namespace Shop.DB.Configurations.OrderMapping
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");

            builder.HasKey(oi => oi.Id);

            builder.Property(oi => oi.Id)
                .HasColumnName("Id")
                .UseIdentityByDefaultColumn();

            builder.Property(oi => oi.OrderId)
                .HasColumnName("OrderId")
                .IsRequired();

            builder.Property(oi => oi.ProductVariantId)
                .HasColumnName("ProductVariantId")
                .IsRequired();

            builder.Property(oi => oi.UnitPrice)
                .HasColumnName("UnitPrice")
                .HasColumnType("numeric(10,2)")
                .IsRequired();

            builder.Property(oi => oi.Quantity)
                .HasColumnName("Quantity")
                .IsRequired();

            builder.Property(oi => oi.OriginalPrice)
                .HasColumnName("OriginalPrice")
                .HasColumnType("numeric(10,2)");

            builder.ToTable("OrderItems", t => t.HasCheckConstraint("CK_OrderItem_Quantity", "\"Quantity\" > 0"));

            builder.HasIndex(oi => new { oi.OrderId, oi.ProductVariantId })
                .IsUnique()
                .HasDatabaseName("IX_OrderItems_OrderId_ProductVariantId");

            builder.HasIndex(oi => oi.ProductVariantId)
                .HasDatabaseName("IX_OrderItems_ProductVariantId");

            builder.HasOne(oi => oi.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_OrderItems_Orders_OrderId");

            builder.HasOne(oi => oi.ProductVariant)
                .WithMany(pv => pv.OrderItems)
                .HasForeignKey(oi => oi.ProductVariantId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_OrderItems_ProductVariants_ProductVariantId");
        }
    }
}
