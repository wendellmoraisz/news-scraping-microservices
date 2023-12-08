using AutoMapper;
using EmailSender.Application.Services;
using EmailSender.Core.Entities;
using EventBus.Messages.Events;
using MassTransit;

namespace EmailSender.API.EventBusConsumer;

public class SendEmailConsumer : IConsumer<SendEmailEvent>
{
    private readonly ISendEmailService _sendEmailService;
    private readonly IMapper _mapper;
    
    public SendEmailConsumer(ISendEmailService sendEmailService, IMapper mapper)
    {
        _sendEmailService = sendEmailService;
        _mapper = mapper;
    }
    
    public async Task Consume(ConsumeContext<SendEmailEvent> context)
    {
        var email = _mapper.Map<Email>(context.Message);
        await _sendEmailService.Send(email);
    }
}