using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.DB.Entities.Catalog.Products.Options;

namespace Shop.DB.Configurations.Catalog.ProductMapping.OptionMapping
{
    public class ProductOptionConfiguration : IEntityTypeConfiguration<ProductOption>
    {
        public void Configure(EntityTypeBuilder<ProductOption> builder)
        {
            builder.ToTable("ProductOptions");

            builder.HasKey(po => po.Id);

            builder.Property(po => po.Id)
                .HasColumnName("Id")
                .UseIdentityByDefaultColumn();

            builder.Property(po => po.Name)
                .HasColumnName("Name")
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(po => po.Name)
                .IsUnique()
                .HasDatabaseName("IX_ProductOptions_Name");
        }
    }
}
