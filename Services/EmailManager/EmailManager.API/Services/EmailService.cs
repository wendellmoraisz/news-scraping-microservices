using EmailManager.Application.UseCases.GetAllEmails;
using EmailManager.Grpc.Protos;
using Grpc.Core;
using MediatR;

namespace EmailManager.API.Services;

public class EmailService : EmailProtoService.EmailProtoServiceBase
{
    private readonly IMediator _mediator;

    public EmailService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<GetAllEmailsResponse> GetEmailsAddress(Empty request, ServerCallContext context)
    {
        var cancellationTokenSource = new CancellationTokenSource();
        var cancelTimeout = TimeSpan.FromSeconds(30);
        cancellationTokenSource.CancelAfter(cancelTimeout);
        
        var query = new GetAllEmailsRequest();
        var result = await _mediator.Send(query, cancellationTokenSource.Token);
        return result;
    }
}