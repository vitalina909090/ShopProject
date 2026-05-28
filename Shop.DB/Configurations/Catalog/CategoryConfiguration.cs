using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.DB.Entities.Catalog;

namespace Shop.DB.Configurations.Catalog
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("Id")
                .UseIdentityByDefaultColumn();

            builder.Property(c => c.Name)
                .HasColumnName("Name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.ParentCategoryId)
                .HasColumnName("ParentCategoryId");

            builder.HasIndex(c => new { c.ParentCategoryId, c.Name })
                .IsUnique()
                .HasDatabaseName("IX_Categories_ParentCategoryId_Name");

            builder.HasOne(c => c.ParentCategory)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Categories_Categories_ParentCategoryId");
        }
    }
}
