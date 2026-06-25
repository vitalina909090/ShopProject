namespace Shop.API.DTOs.Catalog;

public class CatalogResponseDto
{
    public List<ProductCatalogItemDto> Items { get; init; } = [];

    public long TotalCount { get; init; }
}