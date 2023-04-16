using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Scroll.Library.Models;
using Scroll.Library.Models.DTOs;
using Scroll.Library.Models.Entities;
using Scroll.Service.Services;

namespace Scroll.Web.Areas.Admin.Pages.Pictures;

public class IndexModel : PageModel
{
    private readonly IPictureService _pictureService;

    public IndexModel(IPictureService pictureService)
    {
        _pictureService = pictureService;
    }

    public PagedList<ScrollFileInfo> Pictures { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(
        int pageIndex = 0,
        int pageSize = 40)
    {
        Pictures =
            await _pictureService.Get(pageIndex, pageSize);

        return Page();
    }
}
