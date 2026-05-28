using Shop.DB.Entities.Catalog.Products;
using Shop.DB.Entities.Catalog.Products.Images;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.DB.Entities.Catalog.Products.Options
{
    public class ProductOptionValue
    {
        public int Id { get; set; }
        public int ProductOptionId { get; set; }
        public string Value { get; set; } = null!;
        public int SortOrder { get; set; }
        public string? ColorHex { get; set; }

        public ProductOption ProductOption { get; set; } = null!;
        public ICollection<ProductOptionLink> OptionLinks { get; set; } = new List<ProductOptionLink>();
        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
        public ICollection<VariantOptionLink> VariantOptionLinks { get; set; } = new List<VariantOptionLink>();
    }
}
