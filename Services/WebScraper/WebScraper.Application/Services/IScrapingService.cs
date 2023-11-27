using WebScraper.Core.Entities;

namespace WebScraper.Application.Services;

public interface IScrapingService
{
    Task<IEnumerable<News>> ExtractNews();
}