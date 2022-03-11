using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Scroll.Library.Models.EditModels;
using Scroll.Service.Services;

namespace Scroll.Web.Areas.Admin.Pages.Categories;

public class CreateOrEditModel : PageModel
{
    private readonly ICategoryService _categoryService;

    public CreateOrEditModel(
        ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [BindProperty]
    public CategoryEditModel EditModel { get; set; } = new();

    public async Task<ActionResult> OnGetAsync(int? id = null)
    {
        if (id > 0)
        {
            var editModel =
                await _categoryService.GetForEdit(id.Value);

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

        try
        {
            if (EditModel.Id > 0)
            {
                await _categoryService.Update(EditModel);
            }
            else
            {
                await _categoryService.Insert(EditModel);
            }
        }
        catch (DbUpdateException)
        {
            if (await _categoryService.GetByName(EditModel.Name) is not null)
            {
                return BadRequest(
                    $"Category with same name \"{EditModel.Name}\" already exists.");
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }
}
