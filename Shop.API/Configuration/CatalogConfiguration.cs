namespace Shop.API.Configuration;

public class CatalogConfiguration
{
    public static int Page { get; set; } = 1;
    public static int PageSize { get; set; } = 20;
    public static int MaxPageSize { get; set; } = 100;
}
