using Shop.API.Repositories;
using Shop.API.Repositories.Interfaces;

namespace Shop.API.ProgramExtensions;

public static class RepositoriesExtension
{
    public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();


        return services;
    }
}
