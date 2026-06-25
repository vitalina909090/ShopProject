using Shop.API.DTOs.Catalog;

namespace Shop.API.Repositories.Interfaces;

public interface IProductRepository
{
    Task<List<ProductResponseDto>> GetByIdsAsync(List<int> ids);
    Task<List<ProductCatalogItemDto>> GetCatalogItemsAsync(List<int> ids);
    Task<List<ProductIndexData>> GetAllActiveForIndexAsync();
}