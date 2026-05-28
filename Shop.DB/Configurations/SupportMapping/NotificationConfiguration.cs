using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.DB.Entities.Support;

namespace Shop.DB.Configurations.SupportMapping
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notifications");

            builder.HasKey(n => n.Id);

            builder.Property(n => n.Id)
                .HasColumnName("Id")
                .UseIdentityByDefaultColumn();

            builder.Property(n => n.UserId)
                .HasColumnName("UserId")
                .IsRequired();

            builder.Property(n => n.Message)
                .HasColumnName("Message")
                .HasMaxLength(2000)
                .IsRequired();

            builder.Property(n => n.Type)
                .HasColumnName("Type")
                .IsRequired();

            builder.Property(n => n.Url)
                .HasColumnName("Url");

            builder.Property(n => n.IsRead)
                .HasColumnName("IsRead")
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(n => n.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            builder.HasIndex(n => n.UserId)
                .HasDatabaseName("IX_Notifications_UserId");

            builder.HasIndex(n => new { n.UserId, n.IsRead, n.CreatedAt })
                .HasDatabaseName("IX_Notifications_UserId_IsRead_CreatedAt");

            builder.HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Notifications_Users_UserId");
        }
    }
}
