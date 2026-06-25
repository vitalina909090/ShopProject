using Meilisearch;
using Shop.API.Configuration;
using Shop.API.DTOs.Catalog;
using Shop.API.Exceptions;
using Shop.API.MeilisearchConfig.Models;
using Shop.API.Repositories.Interfaces;

namespace Shop.API.Services.Catalog;

public class MeilisearchProductsService
{
    private readonly Meilisearch.Index index;
    private readonly IProductRepository productRepository;

    public MeilisearchProductsService(MeilisearchClient client, IProductRepository productRepository)
    {
        this.index = client.Index("products");
        this.productRepository = productRepository;
    }

    public async Task<CatalogResponseDto> GetCatalogProductsAsync(int? page = null, int? pageSize = null)
    {
        if (index is null)
            throw new NotFoundException("Индексы товаров в Meilisearch не найдены");
        int currentPage = page ?? CatalogConfiguration.Page;
        currentPage = currentPage < 1 ? 1 : currentPage;

        int currentPageSize = pageSize ?? CatalogConfiguration.PageSize;
        if (currentPageSize < 1)
        {
            currentPageSize = 1;
        }
        else if (currentPageSize > CatalogConfiguration.MaxPageSize)
        {
            currentPageSize = CatalogConfiguration.MaxPageSize;
        }

        var paginatedResult = (PaginatedSearchResult<ProductIndexModel>)await index.SearchAsync<ProductIndexModel>(
            "",
            new SearchQuery
            {
                Page = currentPage,
                HitsPerPage = currentPageSize
            });
        if (!paginatedResult.Hits.Any())
        {
            return new CatalogResponseDto
            {
                Items = [],
                TotalCount = paginatedResult.TotalHits
            };
        }
        var ids = paginatedResult.Hits.Select(h => h.Id).ToList();
        var items = await productRepository.GetCatalogItemsAsync(ids);
        var ordered = ids
            .Select(id => items.FirstOrDefault(p => p.Id == id))
            .Where(p => p is not null)
            .Cast<ProductCatalogItemDto>()
            .ToList();
        return new CatalogResponseDto
        {
            Items = ordered,
            TotalCount = paginatedResult.TotalHits
        };
    }
}