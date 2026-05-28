using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.DB.Entities.Catalog.Products.Options;

namespace Shop.DB.Configurations.Catalog.ProductMapping.OptionMapping
{
    public class ProductOptionLinkConfiguration : IEntityTypeConfiguration<ProductOptionLink>
    {
        public void Configure(EntityTypeBuilder<ProductOptionLink> builder)
        {
            builder.ToTable("ProductOptionLinks");

            builder.HasKey(pol => pol.Id);

            builder.Property(pol => pol.Id)
                .HasColumnName("Id")
                .UseIdentityByDefaultColumn();

            builder.Property(pol => pol.ProductId)
                .HasColumnName("ProductId")
                .IsRequired();

            builder.Property(pol => pol.ProductOptionValueId)
                .HasColumnName("ProductOptionValueId")
                .IsRequired();

            builder.HasIndex(pol => new { pol.ProductId, pol.ProductOptionValueId })
                .IsUnique()
                .HasDatabaseName("IX_ProductOptionLinks_ProductId_ProductOptionValueId");

            builder.HasIndex(pol => pol.ProductOptionValueId)
                .HasDatabaseName("IX_ProductOptionLinks_ProductOptionValueId");

            builder.HasOne(pol => pol.Product)
                .WithMany(p => p.OptionLinks)
                .HasForeignKey(pol => pol.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ProductOptionLinks_Products_ProductId");

            builder.HasOne(pol => pol.ProductOptionValue)
                .WithMany(pov => pov.OptionLinks)
                .HasForeignKey(pol => pol.ProductOptionValueId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ProductOptionLinks_ProductOptionValues_ProductOptionValueId");
        }
    }
}
