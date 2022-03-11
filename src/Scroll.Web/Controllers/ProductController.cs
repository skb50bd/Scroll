using Microsoft.AspNetCore.Mvc;
using Scroll.Library.Models;
using Scroll.Library.Models.DTOs;
using Scroll.Library.Models.EditModels;
using Scroll.Service.Services;

namespace Scroll.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductService _productService;

    public ProductController(
        ILogger<ProductController> logger,
        IProductService productService)
    {
        _logger         = logger;
        _productService = productService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> Get(int productId)
    {
        var product =
            await _productService.Get(productId);

        if (product is null)
        {
            return NotFound();
        }
        else
        {
            return Ok(product);
        }
    }

    [HttpGet]
    public Task<PagedList<ProductDto>> Get(
        int pageIndex = 0,
        int pageSize = 40,
        string? searchString = null,
        ProductSortOrder sortBy = ProductSortOrder.IdDesc,
        int? categoryId = null) =>
            _productService.GetPaged(
                pageIndex,
                pageSize,
                searchString,
                sortBy,
                categoryId);

    [HttpPost]
    public async Task<ActionResult<ProductDto>> Post(ProductEditModel model)
    {
        if (ModelState.IsValid is false)
        {
            return BadRequest(model);
        }

        var product = await _productService.Insert(model);

        return product;
    }

    [HttpPut]
    public async Task<ActionResult<ProductDto>> Put(ProductEditModel model)
    {
        if (ModelState.IsValid is false)
        {
            return BadRequest(model);
        }

        var product = await _productService.Update(model);

        return product;
    }

    [HttpDelete]
    public Task<bool> Delete(int productId) =>
        _productService.Delete(productId);

    [HttpPost("Clicked/{productId}")]
    public Task<int?> Clicked(int productId) =>
        _productService.IncrementClickedCount(productId);
}