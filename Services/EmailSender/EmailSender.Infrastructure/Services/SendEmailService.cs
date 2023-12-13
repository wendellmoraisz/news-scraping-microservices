using System.Net;
using System.Net.Mail;
using EmailSender.Application.Services;
using EmailSender.Core.Entities;
using Microsoft.Extensions.Options;

namespace EmailSender.Infrastructure.Services;

public class SendEmailService : ISendEmailService
{
    private readonly SmtpClient _smtpClient;
    private readonly SmtpOptions _smtpOptions;
    
    public SendEmailService(IOptions<SmtpOptions> options)
    {
        _smtpOptions = options.Value;
        _smtpClient = new SmtpClient(_smtpOptions.Host)
        {
            Port = _smtpOptions.Port,
            Credentials = new NetworkCredential(_smtpOptions.SenderCredentials.Email, _smtpOptions.SenderCredentials.Password),
            EnableSsl = _smtpOptions.EnableSsl,
        };
    }
    
    public async Task Send(Email email)
    {
        try
        {
            foreach (var emailRecipient in email.Recipients)
            {
                var mailMessage = new MailMessage(_smtpOptions.SenderCredentials.Email, emailRecipient, email.Subject, email.Body);
                mailMessage.IsBodyHtml = email.IsBodyHtml;
                await _smtpClient.SendMailAsync(mailMessage);
            }

            Console.WriteLine("E-mails enviados com sucesso!");
        }
        
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao enviar e-mail: {ex}");
        }
    }
}