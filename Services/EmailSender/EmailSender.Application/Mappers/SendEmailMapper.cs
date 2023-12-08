using AutoMapper;
using EmailSender.Core.Entities;
using EventBus.Messages.Events;

namespace EmailSender.Application.Mappers;

public class SendEmailMapper : Profile
{
    public SendEmailMapper()
    {
        CreateMap<Email, SendEmailEvent>().ReverseMap();
    }
}