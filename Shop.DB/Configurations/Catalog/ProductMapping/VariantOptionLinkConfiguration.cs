using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.DB.Entities.Catalog.Products;

namespace Shop.DB.Configurations.Catalog.ProductMapping
{
    public class VariantOptionLinkConfiguration : IEntityTypeConfiguration<VariantOptionLink>
    {
        public void Configure(EntityTypeBuilder<VariantOptionLink> builder)
        {
            builder.ToTable("VariantOptionLinks");

            builder.HasKey(vol => vol.Id);

            builder.Property(vol => vol.Id)
                .HasColumnName("Id")
                .UseIdentityByDefaultColumn();

            builder.Property(vol => vol.ProductVariantId)
                .HasColumnName("ProductVariantId")
                .IsRequired();

            builder.Property(vol => vol.ProductOptionLinkId)
                .HasColumnName("ProductOptionLinkId")
                .IsRequired();

            builder.Property(vol => vol.ProductOptionId)
                .HasColumnName("ProductOptionId")
                .IsRequired();

            builder.Property(vol => vol.ProductOptionValueId)
                .HasColumnName("ProductOptionValueId");

            builder.HasIndex(vol => new { vol.ProductVariantId, vol.ProductOptionId })
                .IsUnique()
                .HasDatabaseName("IX_VariantOptionLinks_ProductVariantId_ProductOptionId");

            builder.HasIndex(vol => new { vol.ProductVariantId, vol.ProductOptionLinkId })
                .IsUnique()
                .HasDatabaseName("IX_VariantOptionLinks_ProductVariantId_ProductOptionLinkId");

            builder.HasIndex(vol => vol.ProductOptionLinkId)
                .HasDatabaseName("IX_VariantOptionLinks_ProductOptionLinkId");

            builder.HasIndex(vol => vol.ProductOptionValueId)
                .HasDatabaseName("IX_VariantOptionLinks_ProductOptionValueId");

            builder.HasOne(vol => vol.ProductVariant)
                .WithMany(pv => pv.VariantOptionLinks)
                .HasForeignKey(vol => vol.ProductVariantId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_VariantOptionLinks_ProductVariants_ProductVariantId");

            builder.HasOne(vol => vol.ProductOptionLink)
                .WithMany(pol => pol.VariantOptionLinks)
                .HasForeignKey(vol => vol.ProductOptionLinkId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_VariantOptionLinks_ProductOptionLinks_ProductOptionLinkId");

            builder.HasOne(vol => vol.ProductOptionValue)
                .WithMany(pov => pov.VariantOptionLinks)
                .HasForeignKey(vol => vol.ProductOptionValueId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_VariantOptionLinks_ProductOptionValues_ProductOptionValueId");
        }
    }
}
