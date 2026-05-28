
namespace Shop.DB.Entities.Catalog.Products.Attributes
{
    public class ProductAttributeValue
    {
        public int Id { get; set; }
        public int ProductAttributeId { get; set; }
        public string Value { get; set; } = null!;

        public ProductAttribute ProductAttribute { get; set; } = null!;
        public ICollection<ProductAttributeLink> AttributeLinks { get; set; } = new List<ProductAttributeLink>();
    }
}
