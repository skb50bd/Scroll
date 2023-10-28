using Microsoft.AspNetCore.Identity;

namespace Scroll.Domain.Entities;

public class User : IdentityUser<Guid>, IEntity<Guid>
{
    public string? FullName { get; set; }
    public DateTimeOffset JoinedOn { get; private set; } = DateTimeOffset.UtcNow;
    public bool IsAdmin { get; set; } = false;
}