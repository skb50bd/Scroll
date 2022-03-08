using Microsoft.AspNetCore.Mvc;
using Scroll.Library.Models;
using Scroll.Library.Models.EditModels;
using Scroll.Library.Models.Entities;
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
        _logger = logger;
        _productService   = productService;
    }

    [HttpGet]
    public Task<PagedList<Product>> Get(
        int pageIndex = 0,
        int pageSize = 40) =>
            _productService.GetPaged(pageIndex, pageSize);

    [HttpPost]
    public async Task<ActionResult<Product>> Post(ProductEditModel model)
    {
        if (ModelState.IsValid is false)
        {
            return BadRequest(model);
        }

        var product = await _productService.Insert(model);

        return product;
    }

    [HttpPut]
    public async Task<ActionResult<Product>> Put(ProductEditModel model)
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