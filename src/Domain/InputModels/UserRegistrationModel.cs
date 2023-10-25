namespace Scroll.Domain.InputModels;

public record UserRegistrationModel(
    string FullName,
    string UserName,
    string Email, 
    string Password
);