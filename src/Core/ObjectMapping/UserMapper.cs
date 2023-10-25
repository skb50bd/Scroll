using Riok.Mapperly.Abstractions;
using Scroll.Domain.DTOs;
using Scroll.Domain.Entities;

namespace Scroll.Core.ObjectMapping;

[Mapper]
public static partial class UserMapper
{
    public static partial UserDto ToDto(this User? entity);

    public static async Task<UserDto?> ToDtoAsync(this Task<User?> source)
    {
        var entity = await source;
        return entity.ToDto();
    }
}