using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.DB.Entities.Payments;

namespace Shop.DB.Configurations.PaymentMapping
{
    public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.ToTable("Providers");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("Id")
                .UseIdentityByDefaultColumn();

            builder.Property(p => p.Name)
                .HasColumnName("Name")
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(p => p.Name)
                .IsUnique()
                .HasDatabaseName("IX_Providers_Name");
        }
    }
}
