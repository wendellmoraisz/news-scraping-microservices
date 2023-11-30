using Microsoft.EntityFrameworkCore;
using WebScraper.Core.Entities;

namespace WebScraper.Infrastructure.Context;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    
    public DbSet<News> News { get; set; }
}