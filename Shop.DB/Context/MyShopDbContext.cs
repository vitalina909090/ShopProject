using Microsoft.EntityFrameworkCore;
using Shop.DB.Entities.Carts;
using Shop.DB.Entities.Catalog;
using Shop.DB.Entities.Catalog.Products;
using Shop.DB.Entities.Catalog.Products.Attributes;
using Shop.DB.Entities.Catalog.Products.Images;
using Shop.DB.Entities.Catalog.Products.Options;
using Shop.DB.Entities.Discounts;
using Shop.DB.Entities.Identity;
using Shop.DB.Entities.Orders;
using Shop.DB.Entities.Payments;
using Shop.DB.Entities.Reviews;
using Shop.DB.Entities.Support;
using Shop.DB.Entities.Wishlists;

namespace Shop.DB.Context;

public class MyShopDbContext : DbContext
{
    public MyShopDbContext(DbContextOptions<MyShopDbContext> options) : base(options) { }

    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Provider> Providers => Set<Provider>();
    public DbSet<DiscountType> DiscountTypes => Set<DiscountType>();
    public DbSet<ProductAttribute> ProductAttributes => Set<ProductAttribute>();
    public DbSet<ProductAttributeValue> ProductAttributeValues => Set<ProductAttributeValue>();
    public DbSet<ProductOption> ProductOptions => Set<ProductOption>();
    public DbSet<ProductOptionValue> ProductOptionValues => Set<ProductOptionValue>();

    public DbSet<User> Users => Set<User>();
    public DbSet<CustomerProfile> CustomerProfiles => Set<CustomerProfile>();
    public DbSet<PersonnelProfile> PersonnelProfiles => Set<PersonnelProfile>();

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductVariant> ProductVariants => Set<ProductVariant>();
    public DbSet<ProductImage> ProductImages => Set<ProductImage>();
    public DbSet<ProductAttributeLink> ProductAttributeLinks => Set<ProductAttributeLink>();
    public DbSet<ProductOptionLink> ProductOptionLinks => Set<ProductOptionLink>();
    public DbSet<VariantOptionLink> VariantOptionLinks => Set<VariantOptionLink>();

    public DbSet<Cart> Carts => Set<Cart>();
    public DbSet<CartItem> CartItems => Set<CartItem>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Payment> Payments => Set<Payment>();

    public DbSet<Discount> Discounts => Set<Discount>();
    public DbSet<DiscountProduct> DiscountProducts => Set<DiscountProduct>();

    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<Wishlist> Wishlists => Set<Wishlist>();
    public DbSet<Notification> Notifications => Set<Notification>();

    public DbSet<SupportTicket> SupportTickets => Set<SupportTicket>();
    public DbSet<SupportMessage> SupportMessages => Set<SupportMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyShopDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        base.OnConfiguring(optionsBuilder);
    }
}