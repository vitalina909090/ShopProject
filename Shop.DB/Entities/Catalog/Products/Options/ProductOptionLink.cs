using Shop.DB.Entities.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.DB.Entities.Catalog.Products.Options
{
    public class ProductOptionLink
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ProductOptionValueId { get; set; }

        public Product Product { get; set; } = null!;
        public ProductOptionValue ProductOptionValue { get; set; } = null!;
        public ICollection<VariantOptionLink> VariantOptionLinks { get; set; } = new List<VariantOptionLink>();
    }
}
