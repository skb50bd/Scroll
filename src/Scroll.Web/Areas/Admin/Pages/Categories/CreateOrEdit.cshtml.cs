using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        if (EditModel.Id > 0)
        {
            await _categoryService.Update(EditModel);
        }
        else
        {
            await _categoryService.Insert(EditModel);
        }

        return RedirectToPage("./Index");
    }
}
