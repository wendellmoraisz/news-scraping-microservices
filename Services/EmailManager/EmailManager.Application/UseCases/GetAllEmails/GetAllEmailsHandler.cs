using AutoMapper;
using EmailManager.Application.Repositories;
using EmailManager.Grpc.Protos;
using MediatR;

namespace EmailManager.Application.UseCases.GetAllEmails;

public class GetAllEmailsHandler : IRequestHandler<GetAllEmailsRequest, GetAllEmailsResponse>
{
    private readonly IEmailRepository _emailRepository;
    private readonly IMapper _mapper;

    public GetAllEmailsHandler(IEmailRepository emailRepository, IMapper mapper)
    {
        _emailRepository = emailRepository;
        _mapper = mapper;
    }
    
    public async Task<GetAllEmailsResponse> Handle(GetAllEmailsRequest request, CancellationToken cancellationToken)
    {
        var emails = await _emailRepository.GetAll(cancellationToken);
        return _mapper.Map<GetAllEmailsResponse>(emails);
    }
}