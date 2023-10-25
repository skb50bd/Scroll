namespace Scroll.Api.Services;

public class FakeEmailSender
{
    public Task SendEmailAsync(
        string email,
        string subject,
        string htmlMessage
    )
    {
        return Task.CompletedTask;
    }
}