using Microsoft.AspNetCore.Mvc;
using Shop.API.DTOs.Catalog;
using Shop.API.Services.Catalog;

namespace Shop.API.Controllers.Catalog;

[ApiController]
[Route("api/[controller]")]
public class CatalogController : ControllerBase
{
    private readonly MeilisearchProductsService meilisearchProductsService;

    public CatalogController(MeilisearchProductsService meilisearchProductsService)
    {
        this.meilisearchProductsService = meilisearchProductsService;
    }

    [HttpGet("products")]
    public async Task<ActionResult<CatalogResponseDto>> GetProducts([FromQuery] int? page, [FromQuery] int? pageSize)
    {
        var result = await meilisearchProductsService.GetCatalogProductsAsync(page, pageSize);
        return Ok(result);
    }
}