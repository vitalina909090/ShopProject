using Mapster;
using Shop.API.DTOs.Catalog;
using Shop.DB.Entities.Catalog.Products;
using Shop.DB.Entities.Discounts;

namespace Shop.API.Mapping;

public class ProductMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Product, ProductResponseDto>()
            .Map(dest => dest.CategoryName, src => src.Category.Name)
            .Map(dest => dest.DefaultVariantId, src =>
                src.CatalogVariant != null ? src.CatalogVariant.Id : 0)
            .Map(dest => dest.Price, src =>
                src.CatalogVariant != null ? src.CatalogVariant.Price : 0)
            .Map(dest => dest.ImageUrl, src => ProductMappingConfig.GetMainImageUrl(src))
            .Map(dest => dest.Colors, src => ProductMappingConfig.GetProductColors(src))
            .Map(dest => dest.HasDiscount, src => ProductMappingConfig.HasDiscount(src))
            .Map(dest => dest.DiscountForm, src =>
                ProductMappingConfig.ProductActiveDiscounts(src)
                    .OrderBy(d => ProductMappingConfig.CalculateDiscountedPrice(
                        src.CatalogVariant != null ? src.CatalogVariant.Price : 0, d))
                    .Select(d => (int?)d.Form)
                    .FirstOrDefault())
            .Map(dest => dest.DiscountValue, src =>
                ProductMappingConfig.ProductActiveDiscounts(src)
                    .OrderBy(d => ProductMappingConfig.CalculateDiscountedPrice(
                        src.CatalogVariant != null ? src.CatalogVariant.Price : 0, d))
                    .Select(d => (decimal?)d.Value)
                    .FirstOrDefault())
            .Map(dest => dest.DiscountedPrice, src =>
                ProductMappingConfig.ProductActiveDiscounts(src)
                    .OrderBy(d => ProductMappingConfig.CalculateDiscountedPrice(
                        src.CatalogVariant != null ? src.CatalogVariant.Price : 0, d))
                    .Select(d => (decimal?)ProductMappingConfig.CalculateDiscountedPrice(
                        src.CatalogVariant != null ? src.CatalogVariant.Price : 0, d))
                    .FirstOrDefault());
    }

    public static IEnumerable<Shop.DB.Entities.Discounts.Discount> ProductActiveDiscounts(Product src)
    {
        var now = DateTimeOffset.UtcNow;

        var fromProduct = src.DiscountProducts
            .Select(dp => dp.Discount)
            .Where(d => ProductMappingConfig.IsDiscountActive(d, now));
        var fromCategory = src.Category.Discounts
            .Where(d => ProductMappingConfig.IsDiscountActive(d, now));

        return fromProduct.Any() ? fromProduct : fromCategory;
    }

    public static decimal CalculateDiscountedPrice(decimal price, Shop.DB.Entities.Discounts.Discount d) =>
        d.Form == 0 ? price * (1 - d.Value / 100m) : price - d.Value;

    private static bool ColorHasDiscount(VariantOptionLink vol, Product src)
    {
        var now = DateTimeOffset.UtcNow;

        return vol.ProductVariant.Discounts.Any(d => ProductMappingConfig.IsDiscountActive(d, now)) ||
            src.DiscountProducts.Any(dp => ProductMappingConfig.IsDiscountActive(dp.Discount, now)) ||
            src.Category.Discounts.Any(d => ProductMappingConfig.IsDiscountActive(d, now));
    }

    private static decimal? ColorDiscountedPrice(VariantOptionLink vol, Product src)
    {
        var now = DateTimeOffset.UtcNow;
        var price = vol.ProductVariant.Price;

        var prices = new List<decimal?>();

        prices.AddRange(vol.ProductVariant.Discounts
            .Where(d => ProductMappingConfig.IsDiscountActive(d, now))
            .Select(d => (decimal?)CalculateDiscountedPrice(price, d)));

        prices.AddRange(src.DiscountProducts
            .Where(dp => ProductMappingConfig.IsDiscountActive(dp.Discount, now))
            .Select(dp => (decimal?)CalculateDiscountedPrice(price, dp.Discount)));

        prices.AddRange(src.Category.Discounts
            .Where(d => ProductMappingConfig.IsDiscountActive(d, now))
            .Select(d => (decimal?)CalculateDiscountedPrice(price, d)));

        return prices.Any() ? prices.Min() : null;
    }

    public static string GetMainImageUrl(Product src)
    {
        var defaultVariant = src.CatalogVariant;
        if (defaultVariant != null)
        {
            var colorLink = defaultVariant.VariantOptionLinks
                .FirstOrDefault(vol =>
                    vol.ProductOptionLink != null &&
                    vol.ProductOptionLink.ProductOptionValue != null &&
                    vol.ProductOptionLink.ProductOptionValue.ProductOption != null &&
                    vol.ProductOptionLink.ProductOptionValue.ProductOption.Name.ToLower() == "color");

            if (colorLink != null && colorLink.ProductOptionValueId.HasValue)
            {
                var image = src.Images
                    .Where(i => i.ProductOptionValueId == colorLink.ProductOptionValueId.Value)
                    .OrderBy(i => i.SortOrder)
                    .Select(i => i.Url)
                    .FirstOrDefault();

                if (!string.IsNullOrEmpty(image))
                    return image;
            }
        }

        return src.Images
            .OrderBy(i => i.SortOrder)
            .Select(i => i.Url)
            .FirstOrDefault() ?? string.Empty;
    }

    public static bool IsDiscountActive(Discount discount, DateTimeOffset now)
    {
        return discount.IsActive &&
               (discount.StartAt == null || discount.StartAt <= now) &&
               (discount.EndAt == null || discount.EndAt >= now);
    }

    public static List<ProductColorDto> GetProductColors(Product src)
    {
        var colorLinks = src.Variants
            .SelectMany(v => v.VariantOptionLinks)
            .Where(vol =>
                vol.ProductOptionLink != null &&
                vol.ProductOptionLink.ProductOptionValue != null &&
                vol.ProductOptionLink.ProductOptionValue.ProductOption != null &&
                vol.ProductOptionLink.ProductOptionValue.ProductOption.Name.ToLower() == "color")
            .GroupBy(vol => vol.ProductOptionValueId ?? 0)
            .Select(g => g
                .OrderBy(vol => vol.ProductVariant.Price)
                .First())
            .ToList();

        return colorLinks
            .Select(vol => new ProductColorDto
            {
                OptionValueId = vol.ProductOptionValueId ?? 0,
                Name = vol.ProductOptionLink.ProductOptionValue!.Value,
                Hex = vol.ProductOptionLink.ProductOptionValue!.ColorHex ?? "#000000",
                ImageUrl = src.Images
                    .Where(i => i.ProductOptionValueId == vol.ProductOptionValueId)
                    .Select(i => i.Url)
                    .FirstOrDefault() ?? string.Empty,
                Price = vol.ProductVariant.Price,
                HasDiscount = ProductMappingConfig.ColorHasDiscount(vol, src),
                DiscountedPrice = ProductMappingConfig.ColorDiscountedPrice(vol, src)
            })
            .OrderBy(c => c.Price)
            .ToList();
    }

    public static bool HasDiscount(Product src)
    {
        var now = DateTimeOffset.UtcNow;
        return src.DiscountProducts.Any(dp => ProductMappingConfig.IsDiscountActive(dp.Discount, now)) ||
               src.Category.Discounts.Any(d => ProductMappingConfig.IsDiscountActive(d, now));
    }
}