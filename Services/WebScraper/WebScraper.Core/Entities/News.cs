namespace WebScraper.Core.Entities;

public sealed class News : BaseEntity
{
    public string Title { get; set; }
    public string Content { get; set; }
}