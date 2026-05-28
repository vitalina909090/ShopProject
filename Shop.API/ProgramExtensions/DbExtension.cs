namespace Shop.API.ProgramExtensions;

public static class DbExtension
{
    public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration config)
    {

        var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

        return services;
    }
}