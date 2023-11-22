using AutoMapper;
using EmailManager.Application.Responses;
using EmailManager.Core.Entities;

namespace EmailManager.Application.Mappers;

public class EmailMappingProfile : Profile
{
    public EmailMappingProfile()
    {
        CreateMap<Email, EmailResponse>().ReverseMap();
    }
}