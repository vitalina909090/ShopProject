using Meilisearch;

namespace Shop.API.ProgramExtensions;

public static class MeilisearchExtension
{
    public static IServiceCollection AddMeilisearchServices(this IServiceCollection services)
    {
        var masterKey = Environment.GetEnvironmentVariable("MEILI_MASTER_KEY");

        services.AddSingleton(new MeilisearchClient("http://localhost:7700", masterKey));

        return services;
    }
    public static async Task ConfigureIndexAsync(this MeilisearchClient client)
    {
        var index = client.Index("products");

        await Task.CompletedTask;
    }
}