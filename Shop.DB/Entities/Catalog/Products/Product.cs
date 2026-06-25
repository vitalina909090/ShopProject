using Shop.DB.Entities.Catalog.Products.Attributes;
using Shop.DB.Entities.Catalog.Products.Images;
using Shop.DB.Entities.Catalog.Products.Options;
using Shop.DB.Entities.Discounts;
using Shop.DB.Entities.Reviews;
using Shop.DB.Entities.Wishlists;

namespace Shop.DB.Entities.Catalog.Products
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool IsArchived { get; set; }
        public bool IsNew { get; set; }
        public bool IsPopular { get; set; }
        public int? CatalogVariantId { get; set; }
        public int CategoryId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public Category Category { get; set; } = null!;
        public ProductVariant? CatalogVariant { get; set; }
        public ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
        public ICollection<ProductAttributeLink> AttributeLinks { get; set; } = new List<ProductAttributeLink>();
        public ICollection<ProductOptionLink> OptionLinks { get; set; } = new List<ProductOptionLink>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
        public ICollection<DiscountProduct> DiscountProducts { get; set; } = new List<DiscountProduct>();
    }
}
