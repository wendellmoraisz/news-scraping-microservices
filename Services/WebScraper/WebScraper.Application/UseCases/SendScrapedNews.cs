using EmailManager.Grpc.Protos;
using EventBus.Messages.Events;
using MassTransit;
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
    private readonly IPublishEndpoint _publishEndpoint;

    public SendScrapedNews(
        IScrapingService scrapingService,
        INewsRepository newsRepository,
        EmailManagerGrpcService emailManagerGrpcService,
        IUnityOfWork unityOfWork,
        IPublishEndpoint publishEndpoint
    )
    {
        _scrapingService = scrapingService;
        _newsRepository = newsRepository;
        _emailManagerGrpcService = emailManagerGrpcService;
        _unityOfWork = unityOfWork;
        _publishEndpoint = publishEndpoint;
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
            
            var eventMessage = new SendEmailEvent()
            {
                Subject = news.Title,
                Body = news.Content,
                EmailsAddress = emailsList.Emails.Select(email => email.Address),
                IsBodyHtml = true
            };
            await _publishEndpoint.Publish(eventMessage, cancellationToken);
        }
    }
}