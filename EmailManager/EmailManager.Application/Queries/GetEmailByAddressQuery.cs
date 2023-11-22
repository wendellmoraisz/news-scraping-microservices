using EmailManager.Application.Responses;
using MediatR;

namespace EmailManager.Application.Queries;

public class GetEmailByAddressQuery : IRequest<EmailResponse>
{
    public string Address { get; set; }
}