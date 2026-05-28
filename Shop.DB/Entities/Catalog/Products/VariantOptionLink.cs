using Shop.DB.Entities.Catalog.Products.Options;

namespace Shop.DB.Entities.Catalog.Products
{
    public class VariantOptionLink
    {
        public int Id { get; set; }
        public int ProductVariantId { get; set; }
        public int ProductOptionLinkId { get; set; }
        public int ProductOptionId { get; set; }
        public int? ProductOptionValueId { get; set; }

        public ProductVariant ProductVariant { get; set; } = null!;
        public ProductOptionLink ProductOptionLink { get; set; } = null!;
        public ProductOptionValue? ProductOptionValue { get; set; }
    }
}
