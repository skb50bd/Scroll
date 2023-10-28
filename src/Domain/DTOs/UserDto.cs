namespace Scroll.Domain.DTOs;

public record UserDto(
    Guid Id,
    string Email,
    string FullName,
    string UserName,
    DateTimeOffset JoinedOn
);