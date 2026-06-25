namespace Shop.API.DTOs.Catalog;

public record ProductColorDto
{
    public int OptionValueId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Hex { get; init; } = string.Empty;
    public string ImageUrl { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public decimal? DiscountedPrice { get; init; }
    public bool HasDiscount { get; init; }
}