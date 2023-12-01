using Quartz;
using WebScraper.Application.UseCases;

namespace WebScraper.Application.ScheduledJobs;

public class ScrapNewsScheduler : IJob
{
    private readonly SendScrapedNews _sendScrapedNews;

    public ScrapNewsScheduler(SendScrapedNews sendScrapedNews)
    {
        _sendScrapedNews = sendScrapedNews;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        await _sendScrapedNews.Execute(CancellationToken.None);
    }
}