using Microsoft.AspNetCore.Identity;

namespace Scroll.Library.Models.Entities;

public class AppUser : IdentityUser<int>
{
    public string FullName { get; set; } = string.Empty;
}
