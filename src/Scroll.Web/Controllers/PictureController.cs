using Microsoft.AspNetCore.Mvc;
using Scroll.Library.Models;
using Scroll.Library.Models.DTOs;
using Scroll.Service.Services;

namespace Scroll.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PictureController : ControllerBase
{
    private readonly IPictureService _pictureService;

    public PictureController(
        IPictureService pictureService)
    {
        _pictureService = pictureService;
    }

    [HttpGet("List")]
    public Task<PagedList<PictureInfo>> GetAll(
        int pageIndex = 0,
        int pageSize = 10) =>
            _pictureService.Get(pageIndex, pageSize);

    [HttpGet]
    public async Task<ActionResult> Get(string name)
    {
        var picture =
            await _pictureService.Get(name);

        if (picture is null)
        {
            return NotFound();
        }

        return File(
                picture.Stream,
                picture.ContentType,
                picture.Name);
    }

    private readonly HashSet<string> Extensions =
        new()
        {
            ".jpg",
            ".jpeg",
            ".gif",
            ".png",
            ".webp"
        };

    [HttpPost]
    public async Task<ActionResult> Post(
        string name,
        IFormFile file)
    {
        if (file.Length > 0)
        {
            using var ms =
                new MemoryStream();

            var extension =
                Path.GetExtension(file.FileName);

            if (Extensions.Contains(extension) is false)
            {
                return BadRequest(
                    $"File with extension \"{extension}\" not supported.");
            }

            await file.CopyToAsync(ms);

            var fileName =
                await _pictureService.Add(
                    name,
                    ms.ToArray());

            return Ok(fileName);
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpDelete]
    public Task Delete(string name) =>
        _pictureService.Delete(name);

    [HttpDelete("Clean")]
    public Task DeleteFilesWithoutReference() =>
        _pictureService.DeleteFilesWithoutReference();
}