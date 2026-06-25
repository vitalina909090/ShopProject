namespace Shop.API.DTOs.Catalog;

public record ProductCatalogItemDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? CategoryName { get; init; }
    public bool IsNew { get; init; }
    public bool IsPopular { get; init; }
    public int DefaultVariantId { get; init; }
    public decimal Price { get; init; }
    public decimal? DiscountedPrice { get; init; }
    public string ImageUrl { get; init; } = string.Empty;
    public List<ProductColorDto> Colors { get; init; } = [];
    public bool HasDiscount { get; init; }
    public int? DiscountForm { get; init; }
    public decimal? DiscountValue { get; init; }
}
