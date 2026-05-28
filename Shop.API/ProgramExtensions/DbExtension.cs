using Microsoft.EntityFrameworkCore;
using Shop.DB.Context;

namespace Shop.API.ProgramExtensions;

public static class DbExtension
{
    public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration config)
    {

        var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

        services.AddDbContext<MyShopDbContext>(options =>
            options.UseNpgsql(connectionString, b =>
                b.MigrationsAssembly("Shop.DB")));

        return services;
    }
}