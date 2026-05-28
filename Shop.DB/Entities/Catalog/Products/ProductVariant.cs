using Shop.DB.Entities.Carts;
using Shop.DB.Entities.Orders;
using Shop.DB.Entities.Discounts;

namespace Shop.DB.Entities.Catalog.Products
{
    public class ProductVariant
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string VariantKey { get; set; } = null!;

        public Product Product { get; set; } = null!;
        public ICollection<Product> CatalogForProducts { get; set; } = new List<Product>();
        public ICollection<VariantOptionLink> VariantOptionLinks { get; set; } = new List<VariantOptionLink>();
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ICollection<Discount> Discounts { get; set; } = new List<Discount>();
    }
}
