using Shop.API.DTOs.Catalog;
using Shop.DB.Entities.Catalog.Products;

namespace Shop.API.QueryableData
{
    public static class ProductData
    {
        private static readonly DateTimeOffset Now = DateTimeOffset.UtcNow;

        public static IQueryable<ProductResponseDto> SelectProductData(this IQueryable<Product> query)
        {
            return query.Select(p => new ProductResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                IsNew = p.IsNew,
                IsPopular = p.IsPopular,
                CategoryName = p.Category.Name,

                DefaultVariantId = p.CatalogVariant != null ? p.CatalogVariant.Id : 0,
                Price = p.CatalogVariant != null ? p.CatalogVariant.Price : 0,

                ImageUrl = p.Images
                    .Where(i => i.ProductOptionValueId == null)
                    .OrderBy(i => i.SortOrder)
                    .Select(i => i.Url)
                    .FirstOrDefault() ?? string.Empty,

                Colors = p.Variants
                    .SelectMany(v => v.VariantOptionLinks)
                    .Where(vol =>
                        vol.ProductOptionLink.ProductOptionValue != null &&
                        vol.ProductOptionLink.ProductOptionValue.ProductOption.Name.ToLower() == "color")
                    .Select(vol => new ProductColorDto
                    {
                        OptionValueId = vol.ProductOptionValueId ?? 0,
                        Name = vol.ProductOptionLink.ProductOptionValue!.Value,
                        Hex = vol.ProductOptionLink.ProductOptionValue!.ColorHex ?? "#000000",
                        ImageUrl = p.Images
                            .Where(i => i.ProductOptionValueId == vol.ProductOptionValueId)
                            .Select(i => i.Url)
                            .FirstOrDefault() ?? string.Empty,
                        Price = vol.ProductVariant.Price,

                        HasDiscount =
                            vol.ProductVariant.Discounts.Any(d =>
                                d.IsActive &&
                                (d.StartAt == null || d.StartAt <= DateTimeOffset.UtcNow) &&
                                (d.EndAt == null || d.EndAt >= DateTimeOffset.UtcNow)) ||
                            p.Category.Discounts.Any(d =>
                                d.IsActive &&
                                (d.StartAt == null || d.StartAt <= DateTimeOffset.UtcNow) &&
                                (d.EndAt == null || d.EndAt >= DateTimeOffset.UtcNow)),

                        DiscountedPrice =
                            vol.ProductVariant.Discounts
                                .Where(d =>
                                    d.IsActive &&
                                    (d.StartAt == null || d.StartAt <= DateTimeOffset.UtcNow) &&
                                    (d.EndAt == null || d.EndAt >= DateTimeOffset.UtcNow))
                                .OrderBy(d => d.Form == 0
                                    ? vol.ProductVariant.Price * (1 - d.Value / 100m)
                                    : vol.ProductVariant.Price - d.Value)
                                .Select(d => (decimal?)(d.Form == 0
                                    ? vol.ProductVariant.Price * (1 - d.Value / 100m)
                                    : vol.ProductVariant.Price - d.Value))
                                .FirstOrDefault() ??
                            p.Category.Discounts
                                .Where(d =>
                                    d.IsActive &&
                                    (d.StartAt == null || d.StartAt <= DateTimeOffset.UtcNow) &&
                                    (d.EndAt == null || d.EndAt >= DateTimeOffset.UtcNow))
                                .OrderBy(d => d.Form == 0
                                    ? vol.ProductVariant.Price * (1 - d.Value / 100m)
                                    : vol.ProductVariant.Price - d.Value)
                                .Select(d => (decimal?)(d.Form == 0
                                    ? vol.ProductVariant.Price * (1 - d.Value / 100m)
                                    : vol.ProductVariant.Price - d.Value))
                                .FirstOrDefault()
                    })
                    .DistinctBy(c => c.OptionValueId)
                    .ToList(),

                HasDiscount =
                    p.DiscountProducts.Any(dp =>
                        dp.Discount.IsActive &&
                        (dp.Discount.StartAt == null || dp.Discount.StartAt <= DateTimeOffset.UtcNow) &&
                        (dp.Discount.EndAt == null || dp.Discount.EndAt >= DateTimeOffset.UtcNow)) ||
                    p.Category.Discounts.Any(d =>
                        d.IsActive &&
                        (d.StartAt == null || d.StartAt <= DateTimeOffset.UtcNow) &&
                        (d.EndAt == null || d.EndAt >= DateTimeOffset.UtcNow)),

                DiscountForm =
                    p.DiscountProducts
                        .Where(dp =>
                            dp.Discount.IsActive &&
                            (dp.Discount.StartAt == null || dp.Discount.StartAt <= DateTimeOffset.UtcNow) &&
                            (dp.Discount.EndAt == null || dp.Discount.EndAt >= DateTimeOffset.UtcNow))
                        .OrderBy(dp => dp.Discount.Form == 0
                            ? (p.CatalogVariant != null ? p.CatalogVariant.Price : 0) * (1 - dp.Discount.Value / 100m)
                            : (p.CatalogVariant != null ? p.CatalogVariant.Price : 0) - dp.Discount.Value)
                        .Select(dp => (int?)dp.Discount.Form)
                        .FirstOrDefault() ??
                    p.Category.Discounts
                        .Where(d =>
                            d.IsActive &&
                            (d.StartAt == null || d.StartAt <= DateTimeOffset.UtcNow) &&
                            (d.EndAt == null || d.EndAt >= DateTimeOffset.UtcNow))
                        .OrderBy(d => d.Form == 0
                            ? (p.CatalogVariant != null ? p.CatalogVariant.Price : 0) * (1 - d.Value / 100m)
                            : (p.CatalogVariant != null ? p.CatalogVariant.Price : 0) - d.Value)
                        .Select(d => (int?)d.Form)
                        .FirstOrDefault(),

                DiscountValue =
                    p.DiscountProducts
                        .Where(dp =>
                            dp.Discount.IsActive &&
                            (dp.Discount.StartAt == null || dp.Discount.StartAt <= DateTimeOffset.UtcNow) &&
                            (dp.Discount.EndAt == null || dp.Discount.EndAt >= DateTimeOffset.UtcNow))
                        .OrderBy(dp => dp.Discount.Form == 0
                            ? (p.CatalogVariant != null ? p.CatalogVariant.Price : 0) * (1 - dp.Discount.Value / 100m)
                            : (p.CatalogVariant != null ? p.CatalogVariant.Price : 0) - dp.Discount.Value)
                        .Select(dp => (decimal?)dp.Discount.Value)
                        .FirstOrDefault() ??
                    p.Category.Discounts
                        .Where(d =>
                            d.IsActive &&
                            (d.StartAt == null || d.StartAt <= DateTimeOffset.UtcNow) &&
                            (d.EndAt == null || d.EndAt >= DateTimeOffset.UtcNow))
                        .OrderBy(d => d.Form == 0
                            ? (p.CatalogVariant != null ? p.CatalogVariant.Price : 0) * (1 - d.Value / 100m)
                            : (p.CatalogVariant != null ? p.CatalogVariant.Price : 0) - d.Value)
                        .Select(d => (decimal?)d.Value)
                        .FirstOrDefault(),

                DiscountedPrice =
                    p.DiscountProducts
                        .Where(dp =>
                            dp.Discount.IsActive &&
                            (dp.Discount.StartAt == null || dp.Discount.StartAt <= DateTimeOffset.UtcNow) &&
                            (dp.Discount.EndAt == null || dp.Discount.EndAt >= DateTimeOffset.UtcNow))
                        .OrderBy(dp => dp.Discount.Form == 0
                            ? (p.CatalogVariant != null ? p.CatalogVariant.Price : 0) * (1 - dp.Discount.Value / 100m)
                            : (p.CatalogVariant != null ? p.CatalogVariant.Price : 0) - dp.Discount.Value)
                        .Select(dp => (decimal?)(dp.Discount.Form == 0
                            ? (p.CatalogVariant != null ? p.CatalogVariant.Price : 0) * (1 - dp.Discount.Value / 100m)
                            : (p.CatalogVariant != null ? p.CatalogVariant.Price : 0) - dp.Discount.Value))
                        .FirstOrDefault() ??
                    p.Category.Discounts
                        .Where(d =>
                            d.IsActive &&
                            (d.StartAt == null || d.StartAt <= DateTimeOffset.UtcNow) &&
                            (d.EndAt == null || d.EndAt >= DateTimeOffset.UtcNow))
                        .OrderBy(d => d.Form == 0
                            ? (p.CatalogVariant != null ? p.CatalogVariant.Price : 0) * (1 - d.Value / 100m)
                            : (p.CatalogVariant != null ? p.CatalogVariant.Price : 0) - d.Value)
                        .Select(d => (decimal?)(d.Form == 0
                            ? (p.CatalogVariant != null ? p.CatalogVariant.Price : 0) * (1 - d.Value / 100m)
                            : (p.CatalogVariant != null ? p.CatalogVariant.Price : 0) - d.Value))
                        .FirstOrDefault()
            });
        }
    }
}