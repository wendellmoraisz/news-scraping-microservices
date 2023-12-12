using AutoMapper;
using EmailManager.Core.Entities;

namespace EmailManager.Application.UseCases.CreateEmailAddress;

public sealed class CreateEmailAddressMapper : Profile
{
    public CreateEmailAddressMapper()
    {
        CreateMap<CreateEmailAddressRequest, Email>().ReverseMap();
        CreateMap<Email, CreateEmailAddressResponse>().ReverseMap();
    }
}