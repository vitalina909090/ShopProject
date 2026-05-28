using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.DB.Entities.Discounts;

namespace Shop.DB.Configurations.DiscountMapping
{
    public class DiscountTypeConfiguration : IEntityTypeConfiguration<DiscountType>
    {
        public void Configure(EntityTypeBuilder<DiscountType> builder)
        {
            builder.ToTable("DiscountTypes");

            builder.HasKey(dt => dt.Id);

            builder.Property(dt => dt.Id)
                .HasColumnName("Id")
                .UseIdentityByDefaultColumn();

            builder.Property(dt => dt.Name)
                .HasColumnName("Name")
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(dt => dt.Name)
                .IsUnique()
                .HasDatabaseName("IX_DiscountTypes_Name");
        }
    }
}
