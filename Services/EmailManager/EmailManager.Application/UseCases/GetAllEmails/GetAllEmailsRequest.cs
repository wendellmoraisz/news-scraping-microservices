using EmailManager.Grpc.Protos;
using MediatR;

namespace EmailManager.Application.UseCases.GetAllEmails;

public class GetAllEmailsRequest : IRequest<GetAllEmailsResponse>
{
}