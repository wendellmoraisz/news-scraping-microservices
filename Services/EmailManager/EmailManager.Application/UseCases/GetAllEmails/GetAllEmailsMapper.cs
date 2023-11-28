using AutoMapper;
using EmailManager.Core.Entities;
using EmailManager.Grpc.Protos;

namespace EmailManager.Application.UseCases.GetAllEmails;

public class GetAllEmailsMapper : Profile
{
    public GetAllEmailsMapper()
    {
        CreateMap<List<Email>, GetAllEmailsResponse>().ReverseMap();
    }
}