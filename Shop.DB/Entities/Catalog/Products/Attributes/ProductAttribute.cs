
namespace Shop.DB.Entities.Catalog.Products.Attributes
{
    public class ProductAttribute
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<ProductAttributeValue> Values { get; set; } = new List<ProductAttributeValue>();
    }
}
