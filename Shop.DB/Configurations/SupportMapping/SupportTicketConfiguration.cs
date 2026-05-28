using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.DB.Entities.Support;

namespace Shop.DB.Configurations.SupportMapping
{
    public class SupportTicketConfiguration : IEntityTypeConfiguration<SupportTicket>
    {
        public void Configure(EntityTypeBuilder<SupportTicket> builder)
        {
            builder.ToTable("SupportTickets");

            builder.HasKey(st => st.Id);

            builder.Property(st => st.Id)
                .HasColumnName("Id")
                .UseIdentityByDefaultColumn();

            builder.Property(st => st.UserId)
                .HasColumnName("UserId");

            builder.Property(st => st.Topic)
                .HasColumnName("Topic")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(st => st.Status)
                .HasColumnName("Status")
                .IsRequired();

            builder.Property(st => st.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            builder.Property(st => st.OrderId)
                .HasColumnName("OrderId");

            builder.Property(st => st.GuestEmail)
                .HasColumnName("GuestEmail")
                .HasMaxLength(100);

            builder.Property(st => st.GuestPhone)
                .HasColumnName("GuestPhone")
                .HasMaxLength(20);

            builder.ToTable("SupportTickets", t => t.HasCheckConstraint("CK_SupportTicket_Contact", 
                "(\"UserId\" IS NOT NULL) OR (\"GuestEmail\" IS NOT NULL)"));

            builder.HasIndex(st => st.UserId)
                .HasDatabaseName("IX_SupportTickets_UserId");

            builder.HasIndex(st => st.OrderId)
                .HasDatabaseName("IX_SupportTickets_OrderId");

            builder.HasOne(st => st.User)
                .WithMany(u => u.SupportTickets)
                .HasForeignKey(st => st.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_SupportTickets_Users_UserId");

            builder.HasOne(st => st.Order)
                .WithMany(o => o.SupportTickets)
                .HasForeignKey(st => st.OrderId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_SupportTickets_Orders_OrderId");
        }
    }
}
