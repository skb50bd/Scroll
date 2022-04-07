using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Scroll.Library.Models.DTOs;
using Scroll.Library.Models.EditModels;
using Scroll.Library.Utils;
using Scroll.Service.Services;
using Scroll.Web.Services;

namespace Scroll.Web.Areas.Admin.Pages.Products;

public class CreateOrEditModel : PageModel
{
    private readonly IProductService      _productService;
    private readonly ICategoryService     _categoryService;
    private readonly IPictureService      _pictureService;
    private readonly PictureUploadService _pictureUploadService;

    public CreateOrEditModel(
        IProductService      productService,
        ICategoryService     categoryService,
        IPictureService      pictureService, 
        PictureUploadService pictureUploadService)
    {
        _productService       = productService;
        _categoryService      = categoryService;
        _pictureService       = pictureService;
        _pictureUploadService = pictureUploadService;
    }

    [BindProperty]
    public InputModel EditModel { get; set; } = new();

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

            EditModel.Product = editModel;
        }

        return Page();
    }

    public async Task<ActionResult> OnPostAsync()
    {
        if (ModelState.IsValid is false)
        {
            return Page();
        }

        if (EditModel.HasNewPicture is false
            && EditModel.Product.ImageName.IsNotBlank()
            && await _pictureService.Exists(EditModel.Product.ImageName) is false)
        {
            return BadRequest(
                $"Specified Image \"{EditModel.Product.ImageName}\" doesn't exist.");
        }

        try
        {
            if (EditModel.HasNewPicture)
            {
                try
                {
                    var fileName =
                        await _pictureUploadService.UploadPicture(EditModel.Picture);

                    EditModel.Product.ImageName = fileName;
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            
            if (EditModel.Product.Id > 0)
            {
                await _productService.Update(EditModel.Product);
            }
            else
            {
                await _productService.Insert(EditModel.Product);
            }
        }
        catch (DbUpdateException)
        {
            if (await _productService.GetByTitle(EditModel.Product.Title) is not null)
            {
                return BadRequest(
                    $"Product with same title \"{EditModel.Product.Title}\" already exists.");
            }

            throw;
        }

        return RedirectToPage("./Index");
    }
}

public class InputModel
{
    public ProductEditModel   Product { get; set; } = new();
    public PictureUploadModel Picture { get; set; } = new();
    public bool HasNewPicture => Picture.HasFile;
}