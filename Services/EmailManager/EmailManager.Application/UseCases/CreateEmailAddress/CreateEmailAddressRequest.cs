using MediatR;

namespace EmailManager.Application.UseCases.CreateEmailAddress;

public sealed record CreateEmailAddressRequest(string Address) : IRequest<CreateEmailAddressResponse>;