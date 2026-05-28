using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.DB.Entities.Support;

namespace Shop.DB.Configurations.SupportMapping
{
    public class SupportMessageConfiguration : IEntityTypeConfiguration<SupportMessage>
    {
        public void Configure(EntityTypeBuilder<SupportMessage> builder)
        {
            builder.ToTable("SupportMessages");

            builder.HasKey(sm => sm.Id);

            builder.Property(sm => sm.Id)
                .HasColumnName("Id")
                .UseIdentityByDefaultColumn();

            builder.Property(sm => sm.SupportTicketId)
                .HasColumnName("SupportTicketId")
                .IsRequired();

            builder.Property(sm => sm.Sender)
                .HasColumnName("Sender")
                .IsRequired();

            builder.Property(sm => sm.Message)
                .HasColumnName("Message")
                .HasMaxLength(2000)
                .IsRequired();

            builder.Property(sm => sm.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            builder.HasIndex(sm => sm.SupportTicketId)
                .HasDatabaseName("IX_SupportMessages_SupportTicketId");

            builder.HasOne(sm => sm.SupportTicket)
                .WithMany(st => st.Messages)
                .HasForeignKey(sm => sm.SupportTicketId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_SupportMessages_SupportTickets_SupportTicketId");
        }
    }
}
