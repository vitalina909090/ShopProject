using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Shop.API.DTOs.Catalog;
using Shop.API.Mapping;
using Shop.API.Repositories.Interfaces;
using Shop.DB.Context;
using Shop.DB.Entities.Catalog.Products;

namespace Shop.API.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly MyShopDbContext db;
    private readonly IMapper mapper;

    public ProductRepository(MyShopDbContext db, IMapper mapper)
    {
        this.db = db;
        this.mapper = mapper;
    }

    public async Task<List<ProductResponseDto>> GetByIdsAsync(List<int> ids)
    {
        var products = await db.Products
            .AsSplitQuery()
            .AsNoTracking()
            .Where(p => ids.Contains(p.Id))
            .Include(p => p.Category)
                .ThenInclude(c => c.Discounts)
            .Include(p => p.CatalogVariant)
            .Include(p => p.Images)
            .Include(p => p.DiscountProducts)
                .ThenInclude(dp => dp.Discount)
            .Include(p => p.Variants)
                .ThenInclude(v => v.VariantOptionLinks)
                    .ThenInclude(vol => vol.ProductOptionLink)
                        .ThenInclude(pol => pol.ProductOptionValue)
                            .ThenInclude(pov => pov!.ProductOption)
            .Include(p => p.Variants)
                .ThenInclude(v => v.Discounts)
            .ToListAsync();

        return mapper.Map<List<ProductResponseDto>>(products);
    }

    public async Task<List<ProductCatalogItemDto>> GetCatalogItemsAsync(List<int> ids)
    {
        var now = DateTimeOffset.UtcNow;

        var products = await db.Products
            .AsSplitQuery()
            .AsNoTracking()
            .Where(p => ids.Contains(p.Id))
            .Include(p => p.Category)
                .ThenInclude(c => c.Discounts)
            .Include(p => p.CatalogVariant)
            .Include(p => p.Images)
            .Include(p => p.DiscountProducts)
                .ThenInclude(dp => dp.Discount)
            .Include(p => p.Variants)
                .ThenInclude(v => v.VariantOptionLinks)
                    .ThenInclude(vol => vol.ProductOptionLink)
                        .ThenInclude(pol => pol.ProductOptionValue)
                            .ThenInclude(pov => pov!.ProductOption)
            .Include(p => p.Variants)
                .ThenInclude(v => v.Discounts)
            .ToListAsync();

        return products.Select(p => new ProductCatalogItemDto
        {
            Id = p.Id,
            Name = p.Name,
            CategoryName = p.Category.Name,
            IsNew = p.IsNew,
            IsPopular = p.IsPopular,
            DefaultVariantId = p.CatalogVariant != null ? p.CatalogVariant.Id : 0,
            Price = p.CatalogVariant != null ? p.CatalogVariant.Price : 0,
            ImageUrl = ProductMappingConfig.GetMainImageUrl(p),
            Colors = ProductMappingConfig.GetProductColors(p),
            HasDiscount = ProductMappingConfig.HasDiscount(p),
            DiscountForm = ProductMappingConfig.ProductActiveDiscounts(p)
                .OrderBy(d => ProductMappingConfig.CalculateDiscountedPrice(
                    p.CatalogVariant != null ? p.CatalogVariant.Price : 0, d))
                .Select(d => (int?)d.Form)
                .FirstOrDefault(),
            DiscountValue = ProductMappingConfig.ProductActiveDiscounts(p)
                .OrderBy(d => ProductMappingConfig.CalculateDiscountedPrice(
                    p.CatalogVariant != null ? p.CatalogVariant.Price : 0, d))
                .Select(d => (decimal?)d.Value)
                .FirstOrDefault(),
            DiscountedPrice = ProductMappingConfig.ProductActiveDiscounts(p)
                .OrderBy(d => ProductMappingConfig.CalculateDiscountedPrice(
                    p.CatalogVariant != null ? p.CatalogVariant.Price : 0, d))
                .Select(d => (decimal?)ProductMappingConfig.CalculateDiscountedPrice(
                    p.CatalogVariant != null ? p.CatalogVariant.Price : 0, d))
                .FirstOrDefault()
        }).ToList();
    }

    public async Task<List<ProductIndexData>> GetAllActiveForIndexAsync()
    {
        return await db.Products
            .AsSplitQuery()
            .AsNoTracking()
            .Where(p => !p.IsArchived)
            .Include(p => p.Category)
            .Include(p => p.OptionLinks)
                .ThenInclude(ol => ol.ProductOptionValue)
                    .ThenInclude(pov => pov!.ProductOption)
            .Select(p => new ProductIndexData
            {
                Id = p.Id,
                Name = p.Name,
                CategoryName = p.Category.Name,
                IsNew = p.IsNew,
                IsPopular = p.IsPopular,
                Colors = p.OptionLinks
                    .Where(ol =>
                        ol.ProductOptionValue != null &&
                        ol.ProductOptionValue.ProductOption.Name.ToLower() == "color")
                    .Select(ol => ol.ProductOptionValue!.Value)
                    .Distinct()
                    .ToList()
            })
            .ToListAsync();
    }
}