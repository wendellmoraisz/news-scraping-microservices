using WebScraper.Core.Entities;

namespace WebScraper.Application.Repositories;

public interface INewsRepository : IBaseRepository<News>
{
    Task<News?> GetByTitle(string title, CancellationToken cancellationToken);
}