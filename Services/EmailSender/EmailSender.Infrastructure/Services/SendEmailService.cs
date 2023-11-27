using System.Net;
using System.Net.Mail;
using EmailSender.Application.Services;
using EmailSender.Core.Entities;
using Microsoft.Extensions.Options;

namespace EmailSender.Infrastructure.Services;

public class SendEmailService : ISendEmailService
{
    private readonly SmtpClient _smtpClient;
    
    public SendEmailService(IOptions<SmtpOptions> options)
    {
        var smtpOptions = options.Value;
        _smtpClient = new SmtpClient(smtpOptions.Host)
        {
            Port = smtpOptions.Port,
            Credentials = new NetworkCredential(smtpOptions.SenderCredentials.Email, smtpOptions.SenderCredentials.Password),
            EnableSsl = smtpOptions.EnableSsl,
        };
    }
    
    public async Task Send(Email email)
    {
        try
        {
            var sendEmailsTasks = new List<Task>();
            foreach (var emailRecipient in email.Recipients)
            {
                var mailMessage = new MailMessage(email.Sender.Email, emailRecipient, email.Subject, email.Content);
                mailMessage.IsBodyHtml = email.IsBodyHtml;
                sendEmailsTasks.Add(_smtpClient.SendMailAsync(mailMessage));
            }
            
            await Task.WhenAll(sendEmailsTasks);

            Console.WriteLine("E-mails enviados com sucesso!");
        }
        
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao enviar e-mail: {ex.Message}");
        }
    }
}