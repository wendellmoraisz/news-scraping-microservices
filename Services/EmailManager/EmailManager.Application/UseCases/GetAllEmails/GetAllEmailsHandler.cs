using EmailManager.Application.Repositories;
using EmailManager.Grpc.Protos;
using MediatR;

namespace EmailManager.Application.UseCases.GetAllEmails;

public class GetAllEmailsHandler : IRequestHandler<GetAllEmailsRequest, GetAllEmailsResponse>
{
    private readonly IEmailRepository _emailRepository;

    public GetAllEmailsHandler(IEmailRepository emailRepository)
    {
        _emailRepository = emailRepository;
    }
    
    public async Task<GetAllEmailsResponse> Handle(GetAllEmailsRequest request, CancellationToken cancellationToken)
    {
        var emails = await _emailRepository.GetAll(cancellationToken);
        var emailsResponse = new GetAllEmailsResponse();

        foreach (var email in emails)
        {
            var emailModel = new EmailModel()
            {
                Address = email.Address
            };
            emailsResponse.Emails.Add(emailModel);
        }

        return emailsResponse;
    }
}