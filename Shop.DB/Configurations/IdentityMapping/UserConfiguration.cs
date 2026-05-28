using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.DB.Entities.Identity;

namespace Shop.DB.Configurations.IdentityMapping
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("Id")
                .UseIdentityByDefaultColumn();

            builder.Property(u => u.Username)
                .HasColumnName("Username")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasColumnName("Email")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.PasswordHash)
                .HasColumnName("PasswordHash")
                .IsRequired();

            builder.Property(u => u.RoleId)
                .HasColumnName("RoleId")
                .IsRequired();

            builder.Property(u => u.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            builder.Property(u => u.IsBlocked)
                .HasColumnName("IsBlocked")
                .IsRequired();

            builder.Property(u => u.BlockReason)
                .HasColumnName("BlockReason")
                .HasMaxLength(500);

            builder.HasIndex(u => u.Email)
                .IsUnique()
                .HasDatabaseName("IX_Users_Email");

            builder.HasIndex(u => u.Username)
                .IsUnique()
                .HasDatabaseName("IX_Users_Username");

            builder.HasIndex(u => u.RoleId)
                .HasDatabaseName("IX_Users_RoleId");

            builder.HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Users_Roles_RoleId");
        }
    }
}
