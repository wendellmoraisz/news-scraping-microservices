using AutoMapper;
using EmailManager.Application.Queries;
using EmailManager.Application.Repositories;
using EmailManager.Application.Responses;
using MediatR;

namespace EmailManager.Application.Handlers;

public class GetEmailByAddressHandler : IRequestHandler<GetEmailByAddressQuery, EmailResponse>
{
    private readonly IEmailRepository _emailRepository;
    private readonly IMapper _mapper;

    public GetEmailByAddressHandler(IEmailRepository emailRepository, IMapper mapper)
    {
        _emailRepository = emailRepository;
        _mapper = mapper;
    }
    
    public async Task<EmailResponse> Handle(GetEmailByAddressQuery request, CancellationToken cancellationToken)
    {
        var email = await _emailRepository.GetByAddress(request.Address);
        var emailResponse = _mapper.Map<EmailResponse>(email);
        return emailResponse;
    }
}