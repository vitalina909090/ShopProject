using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.DB.Entities.Identity;

namespace Shop.DB.Configurations.IdentityMapping
{
    public class CustomerProfileConfiguration : IEntityTypeConfiguration<CustomerProfile>
    {
        public void Configure(EntityTypeBuilder<CustomerProfile> builder)
        {
            builder.ToTable("CustomerProfiles");

            builder.HasKey(cp => cp.Id);

            builder.Property(cp => cp.Id)
                .HasColumnName("Id")
                .UseIdentityByDefaultColumn();

            builder.Property(cp => cp.UserId)
                .HasColumnName("UserId")
                .IsRequired();

            builder.Property(cp => cp.PhoneNumber)
                .HasColumnName("PhoneNumber");

            builder.Property(cp => cp.Country)
                .HasColumnName("Country");

            builder.Property(cp => cp.City)
                .HasColumnName("City");

            builder.Property(cp => cp.PostalCode)
                .HasColumnName("PostalCode")
                .HasColumnType("varchar(20)");

            builder.HasIndex(cp => cp.UserId)
                .IsUnique()
                .HasDatabaseName("IX_CustomerProfiles_UserId");

            builder.HasOne(cp => cp.User)
                .WithOne(u => u.CustomerProfile)
                .HasForeignKey<CustomerProfile>(cp => cp.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_CustomerProfiles_Users_UserId");
        }
    }
}
