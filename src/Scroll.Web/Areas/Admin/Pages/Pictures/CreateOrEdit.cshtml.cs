using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Scroll.Library.Utils;
using Scroll.Service.Services;
using System.ComponentModel.DataAnnotations;
using Scroll.Web.Services;

namespace Scroll.Web.Areas.Admin.Pages.Pictures;

public class CreateOrEditModel : PageModel
{
    private readonly PictureUploadService _pictureUploadService;

    public CreateOrEditModel(
        PictureUploadService pictureUploadService)
    {
        _pictureUploadService = pictureUploadService;
    }

    [BindProperty]
    public PictureUploadModel Input { get; set; } = new();

    public IActionResult OnGet()
    {
        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid is false)
        {
            return Page();
        }

        if (Input.HasFile is false)
        {
            return BadRequest("File is Empty");
        }

        try
        {
            await _pictureUploadService.UploadPicture(Input);
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}