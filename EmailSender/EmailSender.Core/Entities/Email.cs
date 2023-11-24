namespace EmailSender.Core.Entities;

public class Email
{
    public EmailSender Sender { get; set; }
    public List<string> Recipients { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
    public bool IsBodyHtml { get; set; }
}