using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.DB.Entities.Identity;

namespace Shop.DB.Configurations.IdentityMapping
{
    public class PersonnelProfileConfiguration : IEntityTypeConfiguration<PersonnelProfile>
    {
        public void Configure(EntityTypeBuilder<PersonnelProfile> builder)
        {
            builder.ToTable("PersonnelProfiles");

            builder.HasKey(pp => pp.Id);

            builder.Property(pp => pp.Id)
                .HasColumnName("Id")
                .UseIdentityByDefaultColumn();

            builder.Property(pp => pp.UserId)
                .HasColumnName("UserId")
                .IsRequired();

            builder.Property(pp => pp.PhoneNumber)
                .HasColumnName("PhoneNumber");

            builder.Property(pp => pp.Department)
                .HasColumnName("Department");

            builder.HasIndex(pp => pp.UserId)
                .IsUnique()
                .HasDatabaseName("IX_PersonnelProfiles_UserId");

            builder.HasOne(pp => pp.User)
                .WithOne(u => u.PersonnelProfile)
                .HasForeignKey<PersonnelProfile>(pp => pp.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PersonnelProfiles_Users_UserId");
        }
    }
}
