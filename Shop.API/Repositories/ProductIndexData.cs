namespace Shop.API.Repositories;

public class ProductIndexData
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? CategoryName { get; init; }
    public bool IsNew { get; init; }
    public bool IsPopular { get; init; }
    public List<string> Colors { get; init; } = [];
}