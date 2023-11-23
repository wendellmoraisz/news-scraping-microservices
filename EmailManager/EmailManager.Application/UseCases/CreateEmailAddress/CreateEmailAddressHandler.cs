using AutoMapper;
using EmailManager.Application.Repositories;
using EmailManager.Core.Entities;
using MediatR;

namespace EmailManager.Application.UseCases.CreateEmailAddress;


public sealed class CreateEmailAddressHandler : IRequestHandler<CreateEmailAddressRequest, CreateEmailAddressResponse>
{
    private readonly IEmailRepository _emailRepository;
    private readonly IMapper _mapper;

    public CreateEmailAddressHandler
    (
        IEmailRepository emailRepository,
        IMapper mapper
    )
    {
        _emailRepository = emailRepository;
        _mapper = mapper;
    }
    
    public async Task<CreateEmailAddressResponse> Handle(CreateEmailAddressRequest request, CancellationToken cancellationToken)
    {
        var email = _mapper.Map<Email>(request);
        await _emailRepository.CreateEmail(email, cancellationToken);

        return _mapper.Map<CreateEmailAddressResponse>(email);
    }
}