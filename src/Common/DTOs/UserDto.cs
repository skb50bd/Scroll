namespace Scroll.Domain.DTOs;

public record UserInfo(Guid Id, string Email);

public record UserDto(
    Guid Id,
    string Email,
    string FullName,
    string UserName,
    DateTimeOffset JoinedOn,
    bool IsAdmin
);