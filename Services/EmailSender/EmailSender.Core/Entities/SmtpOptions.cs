namespace EmailSender.Core.Entities;

public class SmtpOptions
{
    public EmailSender SenderCredentials { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public bool EnableSsl { get; set; }
}