using System.ComponentModel.DataAnnotations;

namespace Scroll.Library.Models.EditModels;

public class CategoryEditModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;
}
