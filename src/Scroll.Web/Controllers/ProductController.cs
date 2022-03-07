using Microsoft.AspNetCore.Mvc;
using Scroll.Library.Models;
using Scroll.Library.Models.EditModels;
using Scroll.Library.Models.Entities;
using Scroll.Service.Data;

namespace Scroll.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    //private readonly ILogger<ProductController> _logger;
    //private readonly ProductRepository _repo;

    //public ProductController(
    //    ILogger<ProductController> logger,
    //    ProductRepository repo)
    //{
    //    _logger = logger;
    //    _repo   = repo;
    //}

    //[HttpGet]
    //public Task<PagedList<Product>> Get(
    //    int pageIndex = 0,
    //    int pageSize = 40) =>
    //        _repo.Get(pageIndex, pageSize);

    //[HttpPost]
    //[HttpPut]
    //public async Task<ActionResult<Product>> Post(ProductEditModel model)
    //{
    //    if (ModelState.IsValid is false)
    //    {
    //        return BadRequest(model);
    //    }

    //    var product = await _repo.Upsert(model);

    //    return product;
    //}

    //[HttpDelete]
    //public Task<bool> Delete(string productId) =>
    //    _repo.Delete(ObjectId.Parse(productId));

    //[HttpPost("{productId}")]
    //public Task<int> Clicked(string productId) =>
    //    _repo.Clicked(ObjectId.Parse(productId));
}