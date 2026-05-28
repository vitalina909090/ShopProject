using Shop.DB.Entities.Catalog.Products;

namespace Shop.DB.Entities.Discounts
{
    public class DiscountProduct
    {
        public int DiscountId { get; set; }
        public int ProductId { get; set; }

        public Discount Discount { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
