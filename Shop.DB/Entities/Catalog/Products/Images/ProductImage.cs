using Shop.DB.Entities.Catalog.Products.Options;

namespace Shop.DB.Entities.Catalog.Products.Images
{
    public class ProductImage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int? ProductOptionValueId { get; set; }
        public string Url { get; set; } = null!;
        public int SortOrder { get; set; }

        public Product Product { get; set; } = null!;
        public ProductOptionValue? ProductOptionValue { get; set; }
    }
}
