namespace Scroll.Api.Services;

public class FakeEmailSender
{
    public Task SendEmailAsync(
        string email,
        string subject,
        string htmlMessage,
        CancellationToken token
    )
    {
        return Task.CompletedTask;
    }
}