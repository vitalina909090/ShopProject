using Shop.API.MeilisearchConfig.Services;
using Shop.API.Services.Catalog;

namespace Shop.API.ProgramExtensions;

public static class ServicesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<MeilisearchProductsService>();
        services.AddScoped<ProductIndexService>();


        return services;
    }

}
