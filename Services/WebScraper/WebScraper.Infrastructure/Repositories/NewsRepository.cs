using Microsoft.EntityFrameworkCore;
using WebScraper.Application.Repositories;
using WebScraper.Core.Entities;
using WebScraper.Infrastructure.Context;

namespace WebScraper.Infrastructure.Repositories;

public class NewsRepository : BaseRepository<News>, INewsRepository
{
    public NewsRepository(DataContext context) : base(context)
    {
    }

    public Task<News?> GetByTitle(string newsTitle, CancellationToken cancellationToken)
    {
        return Context.Set<News>().FirstOrDefaultAsync(x => x.Title == newsTitle, cancellationToken);
    }
}