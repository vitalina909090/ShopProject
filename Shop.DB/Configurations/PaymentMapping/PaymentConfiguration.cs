using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.DB.Entities.Payments;

namespace Shop.DB.Configurations.PaymentMapping
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("Id")
                .UseIdentityByDefaultColumn();

            builder.Property(p => p.OrderId)
                .HasColumnName("OrderId")
                .IsRequired();

            builder.Property(p => p.Amount)
                .HasColumnName("Amount")
                .HasColumnType("numeric(10,2)")
                .IsRequired();

            builder.Property(p => p.Currency)
                .HasColumnName("Currency")
                .HasColumnType("varchar(3)")
                .IsRequired();

            builder.Property(p => p.Status)
                .HasColumnName("Status")
                .IsRequired();

            builder.Property(p => p.ProviderId)
                .HasColumnName("ProviderId");

            builder.Property(p => p.ProviderPaymentId)
                .HasColumnName("ProviderPaymentId");

            builder.Property(p => p.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            builder.ToTable("Payments", t =>
            {
                t.HasCheckConstraint("CK_Payment_Amount", "\"Amount\" > 0");
                t.HasCheckConstraint("CK_Payment_Currency", "LENGTH(\"Currency\") = 3");
            });

            builder.HasIndex(p => p.OrderId)
                .HasDatabaseName("IX_Payments_OrderId");

            builder.HasIndex(p => p.ProviderId)
                .HasDatabaseName("IX_Payments_ProviderId");

            builder.HasOne(p => p.Order)
                .WithMany(o => o.Payments)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Payments_Orders_OrderId");

            builder.HasOne(p => p.Provider)
                .WithMany(pr => pr.Payments)
                .HasForeignKey(p => p.ProviderId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Payments_Providers_ProviderId");
        }
    }
}
