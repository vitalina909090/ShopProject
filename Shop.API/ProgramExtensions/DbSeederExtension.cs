using Shop.API.MeilisearchConfig.Services;
using Shop.DB.Context;
using Shop.DB.Seeders;

namespace Shop.API.ProgramExtensions;

public static class DbSeederExtension
{
    public static Task SeedData(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<MyShopDbContext>();

        DbSeeder.Seed(context);

        return Task.CompletedTask;
    }

    public static async Task IndexProductsIfEmpty(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var productIndexService = scope.ServiceProvider.GetRequiredService<ProductIndexService>();

        await productIndexService.IndexAllIfEmptyAsync();
    }
}