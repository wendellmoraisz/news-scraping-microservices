namespace EmailSender.Core.Entities;

public class Email
{
    public IEnumerable<string> Recipients { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public bool IsBodyHtml { get; set; }
}