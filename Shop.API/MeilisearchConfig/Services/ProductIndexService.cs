using Meilisearch;
using Shop.API.Exceptions;
using Shop.API.MeilisearchConfig.Models;
using Shop.API.Repositories.Interfaces;

namespace Shop.API.MeilisearchConfig.Services;

public class ProductIndexService
{
    private const string IndexName = "products";

    private readonly MeilisearchClient client;
    private readonly Meilisearch.Index index;
    private readonly IProductRepository productRepository;

    public ProductIndexService(MeilisearchClient client, IProductRepository productRepository)
    {
        this.client = client;
        this.index = client.Index(IndexName);
        this.productRepository = productRepository;
    }

    public async Task IndexAllIfEmptyAsync()
    {
        await EnsureIndexExistsAsync();

        var stats = await index.GetStatsAsync();

        if (stats.NumberOfDocuments > 0)
            return;

        await IndexAllAsync();
    }

    public async Task IndexAllAsync()
    {
        await EnsureIndexExistsAsync();

        var products = await productRepository.GetAllActiveForIndexAsync();

        if (products.Count == 0)
        {
            var clearTask = await index.DeleteAllDocumentsAsync();
            await index.WaitForTaskAsync(clearTask.TaskUid);

            return;
        }

        var documents = products.Select(p => new ProductIndexModel
        {
            Id = p.Id,
            Name = p.Name,
            CategoryName = p.CategoryName,
            IsNew = p.IsNew,
            IsPopular = p.IsPopular,
            Colors = p.Colors
        }).ToList();

        var deleteTask = await index.DeleteAllDocumentsAsync();
        await index.WaitForTaskAsync(deleteTask.TaskUid);

        var addTask = await index.AddDocumentsAsync(documents, "id");
        await index.WaitForTaskAsync(addTask.TaskUid);
    }

    private async Task EnsureIndexExistsAsync()
    {
        try
        {
            await client.GetIndexAsync(IndexName);
        }
        catch (MeilisearchApiError)
        {
            var task = await client.CreateIndexAsync(IndexName, "id");
            await client.WaitForTaskAsync(task.TaskUid);
        }
    }
}