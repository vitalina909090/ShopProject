
namespace Shop.DB.Entities.Catalog.Products.Attributes
{
    public class ProductAttributeLink
    {
        public int ProductId { get; set; }
        public int ProductAttributeValueId { get; set; }

        public Product Product { get; set; } = null!;
        public ProductAttributeValue ProductAttributeValue { get; set; } = null!;
    }
}
