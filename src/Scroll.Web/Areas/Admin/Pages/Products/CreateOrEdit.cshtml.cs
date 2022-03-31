using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Scroll.Library.Models.DTOs;
using Scroll.Library.Models.EditModels;
using Scroll.Library.Utils;
using Scroll.Service.Services;

namespace Scroll.Web.Areas.Admin.Pages.Products;

public class CreateOrEditModel : PageModel
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IPictureService _pictureService;

    public CreateOrEditModel(
        IProductService productService,
        ICategoryService categoryService,
        IPictureService pictureService)
    {
        _productService  = productService;
        _categoryService = categoryService;
        _pictureService  = pictureService;
    }

    [BindProperty]
    public ProductEditModel EditModel { get; set; } = new();

    public IList<CategoryDto> AvailableCategories { get; set; } = new List<CategoryDto>();

    public async Task<ActionResult> OnGetAsync(int? id = null)
    {
        var categories =
            await _categoryService.GetPaged(0, int.MaxValue);

        AvailableCategories =
            categories.Items;

        if (id > 0)
        {
            var editModel =
                await _productService.GetForEdit(id.Value);

            if (editModel is null)
            {
                return NotFound();
            }

            EditModel = editModel;
        }

        return Page();
    }

    public async Task<ActionResult> OnPostAsync()
    {
        if (ModelState.IsValid is false)
        {
            return Page();
        }

        if (EditModel.ImageName.IsNotBlank()
            && await _pictureService.Exists(EditModel.ImageName) is false)
        {
            return BadRequest(
                $"Specified Image \"{EditModel.ImageName}\" doesn't exist.");
        }

        try
        {
            if (EditModel.Id > 0)
            {
                await _productService.Update(EditModel);
            }
            else
            {
                await _productService.Insert(EditModel);
            }
        }
        catch (DbUpdateException)
        {
            if (await _productService.GetByTitle(EditModel.Title) is not null)
            {
                return BadRequest(
                    $"Product with same title \"{EditModel.Title}\" already exists.");
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }
}