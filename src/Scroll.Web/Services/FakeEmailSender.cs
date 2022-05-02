﻿using Microsoft.AspNetCore.Identity.UI.Services;

namespace Scroll.Web.Services;

public class FakeEmailSender : IEmailSender
{
    public Task SendEmailAsync(
        string email, 
        string subject, 
        string htmlMessage)
    {
        return Task.CompletedTask;
    }
}