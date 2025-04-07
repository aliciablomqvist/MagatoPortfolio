using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Repositories;
using Magato.Api.Shared;
using System.Net;
using System.Net.Mail;


namespace Magato.Api.Services;


public class EmailService : IEmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendContactNotificationAsync(ContactMessageDto dto)
    {
        var smtp = new SmtpClient(_config["Smtp:Host"])
        {
            Port = int.Parse(_config["Smtp:Port"]),
            Credentials = new NetworkCredential(_config["Smtp:Username"], _config["Smtp:Password"]),
            EnableSsl = true
        };

        var mail = new MailMessage
        {
            From = new MailAddress(_config["Smtp:From"]),
            Subject = "Nytt kontaktmeddelande",
            Body = $"Från: {dto.Name} <{dto.Email}>\n\n{dto.Message}"
        };

        mail.To.Add(_config["Smtp:To"]);

        await smtp.SendMailAsync(mail);
    }
}
