﻿namespace Scroll.Library.Models.Entities;

public sealed class Favorite
{
    public Guid UserId { get; set; }
    public AppUser? User { get; set; }
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
}