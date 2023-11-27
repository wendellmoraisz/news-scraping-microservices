using WebScraper.Core.Entities;

namespace WebScraper.Application.Repositories;

public interface INewsRepository
{
    Task<News?> GetByTitle(string title, CancellationToken cancellationToken);
}