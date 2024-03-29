﻿using System.ComponentModel.DataAnnotations;

namespace Scroll.Domain.InputModels;

public record class CategoryEditModel
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(1000, MinimumLength = 10)]
    public string Description { get; set; } = string.Empty;
}
