using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Scroll.Library.Utils;
using Scroll.Service.Services;
using System.ComponentModel.DataAnnotations;

namespace Scroll.Web.Areas.Admin.Pages.Pictures;

public class CreateOrEditModel : PageModel
{
    private readonly IPictureService _pictureService;

    public CreateOrEditModel(IPictureService pictureService)
    {
        _pictureService = pictureService;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public IActionResult OnGet()
    {
        return Page();
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

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid is false)
        {
            return Page();
        }

        if (Input.Picture?.Length > 0)
        {
            using var ms =
                new MemoryStream();

            var extension =
                Path.GetExtension(Input.Picture.FileName);

            if (Extensions.Contains(extension) is false)
            {
                return BadRequest(
                    $"File with extension \"{extension}\" not supported.");
            }

            await Input.Picture.CopyToAsync(ms);

            var fileName =
                await _pictureService.Add(
                    Input.Name!.ToUrlString(),
                    ms.ToArray(),
                    Input.Width,
                    Input.Height);

            return RedirectToPage("./Index");
        }
        else
        {
            return BadRequest();
        }
    }

    public class InputModel
    {
        public IFormFile? Picture { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string? Name { get; set; }

        [Range(8, 5000)]
        [Display(Name = "Max Width")]
        public int Width { get; set; } = 1024;

        [Range(8, 5000)]
        [Display(Name = "Max Height")]
        public int Height { get; set; } = 1024;
    }
}