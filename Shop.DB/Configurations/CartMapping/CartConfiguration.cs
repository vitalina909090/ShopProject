using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.DB.Entities.Carts;

namespace Shop.DB.Configurations.CartMapping 
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("Id")
                .UseIdentityByDefaultColumn();

            builder.Property(c => c.UserId)
                .HasColumnName("UserId")
                .IsRequired();

            builder.HasIndex(c => c.UserId)
                .IsUnique()
                .HasDatabaseName("IX_Carts_UserId");

            builder.HasOne(c => c.User)
                .WithOne(u => u.Cart)
                .HasForeignKey<Cart>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Carts_Users_UserId");
        }
    }
}
