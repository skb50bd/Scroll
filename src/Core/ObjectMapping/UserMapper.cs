using LanguageExt;
using Riok.Mapperly.Abstractions;
using Scroll.Domain.DTOs;
using Scroll.Domain.Entities;

namespace Scroll.Core.ObjectMapping;

[Mapper]
public static partial class UserMapper
{
    public static partial UserDto? ToDto(this User? entity);
    public static Option<UserDto> ToDto(this Option<User> entity) =>
        entity.Map(x => x.ToDto()).Map(x => x!);

    public static async Task<Option<UserDto>> ToDtoAsync(this Task<Option<User>> source)
    {
        var entity = await source;
        return entity.ToDto();
    }
}