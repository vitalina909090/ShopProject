using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.DB.Entities.Catalog.Products.Options
{
    public class ProductOption
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<ProductOptionValue> Values { get; set; } = new List<ProductOptionValue>();
    }
}
