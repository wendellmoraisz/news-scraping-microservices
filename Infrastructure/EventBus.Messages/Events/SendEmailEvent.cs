namespace EventBus.Messages.Events;

public class SendEmailEvent
{
    public string? Subject { get; set; }
    public string? Body { get; set; }
    public IEnumerable<string>? Recipients { get; set; }
    public bool? IsBodyHtml { get; set; }
}