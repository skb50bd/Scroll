using Microsoft.AspNetCore.Mvc;
using Scroll.Library.Models;
using Scroll.Library.Models.DTOs;
using Scroll.Service.Services;

namespace Scroll.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImageController : ControllerBase
{
    //private readonly ImageService _imageService;

    //public ImageController(
    //    ImageService imageService)
    //{
    //    _imageService = imageService;
    //}

    //[HttpGet("List")]
    //public Task<PagedList<ImageInfo>> GetAll(
    //    int pageIndex = 0,
    //    int pageSize = 10) =>
    //        _imageService.Get(pageIndex, pageSize);

    //[HttpGet]
    //public async Task<ActionResult> Get(string name)
    //{
    //    var image =
    //        await _imageService.Get(name);

    //    if (image is null)
    //    {
    //        return NotFound();
    //    }

    //    return File(image.Data, "image/webp", name);
    //}

    //private readonly HashSet<string> Extensions =
    //    new()
    //    {
    //        ".jpg",
    //        ".jpeg",
    //        ".gif",
    //        ".png",
    //        ".webp"
    //    };

    //[HttpPost]
    //public async Task<ActionResult> Post(
    //    string name,
    //    IFormFile file)
    //{
    //    if (file.Length > 0)
    //    {
    //        using var ms =
    //            new MemoryStream();

    //        var extension =
    //            Path.GetExtension(file.FileName);

    //        if (Extensions.Contains(extension) is false)
    //        {
    //            return BadRequest(
    //                $"File with extension \"{extension}\" not supported.");
    //        }

    //        await file.CopyToAsync(ms);

    //        var fileName =
    //            await _imageService.Add(
    //                name,
    //                ms.ToArray());

    //        return Ok(fileName);
    //    }
    //    else
    //    {
    //        return BadRequest();
    //    }
    //}

    //[HttpDelete]
    //public Task Delete(string name) =>
    //    _imageService.Delete(name);

    //[HttpDelete("Clean")]
    //public Task DeleteFilesWithoutReference() =>
    //    _imageService.DeleteFilesWithoutReference();
}