using WebScraper.Application.Repositories;
using WebScraper.Infrastructure.Context;

namespace WebScraper.Infrastructure.Repositories;

public class UnityOfWork : IUnityOfWork
{
    private readonly DataContext _context;

    public UnityOfWork(DataContext context)
    {
        _context = context;
    }

    public Task Save(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}