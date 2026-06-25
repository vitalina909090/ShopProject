using Shop.DB.Entities.Catalog;
using Shop.DB.Entities.Catalog.Products;

namespace Shop.DB.Entities.Discounts
{
    public class Discount
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int TypeId { get; set; }
        public int Form { get; set; }
        public decimal Value { get; set; }
        public DateTimeOffset? StartAt { get; set; }
        public DateTimeOffset? EndAt { get; set; }
        public bool IsActive { get; set; }
        public int? ProductVariantId { get; set; }
        public int? CategoryId { get; set; }

        public DiscountType DiscountType { get; set; } = null!;
        public ProductVariant? ProductVariant { get; set; }
        public Category? Category { get; set; }
        public ICollection<DiscountProduct> DiscountProducts { get; set; } = new List<DiscountProduct>();
    }
}
