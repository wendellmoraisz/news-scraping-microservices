using EmailManager.Grpc.Protos;
using WebScraper.Application.Repositories;
using WebScraper.Application.Services;
using WebScraper.Application.Services.GrpcServices;

namespace WebScraper.Application.UseCases;

public sealed class SendScrapedNews
{
    private readonly IScrapingService _scrapingService;
    private readonly INewsRepository _newsRepository;
    private readonly EmailManagerGrpcService _emailManagerGrpcService;
    private readonly IUnityOfWork _unityOfWork;

    public SendScrapedNews(
        IScrapingService scrapingService,
        INewsRepository newsRepository,
        EmailManagerGrpcService emailManagerGrpcService,
        IUnityOfWork unityOfWork
    )
    {
        _scrapingService = scrapingService;
        _newsRepository = newsRepository;
        _emailManagerGrpcService = emailManagerGrpcService;
        _unityOfWork = unityOfWork;
    }

    public async Task Execute(CancellationToken cancellationToken)
    {
        var newsList = await _scrapingService.ExtractNews();
        var emailsList = await _emailManagerGrpcService.GetAllEmails(new Empty());

        foreach (var news in newsList)
        {
            var newsIsAlreadySent = await _newsRepository.GetByTitle(news.Title, cancellationToken) != null;
            if (newsIsAlreadySent) continue;
            _newsRepository.Create(news);
            await _unityOfWork.Save(cancellationToken);
            // await _emailService.Send(emailsList, news.Title, news.Content);
        }
    }
}