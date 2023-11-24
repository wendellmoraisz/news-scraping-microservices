using EmailSender.Core.Entities;

namespace EmailSender.Application.Services;

public interface ISendEmailService
{
    Task Send(Email email);
}