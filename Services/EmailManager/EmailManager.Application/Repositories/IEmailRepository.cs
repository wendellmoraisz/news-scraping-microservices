using EmailManager.Core.Entities;

namespace EmailManager.Application.Repositories;

public interface IEmailRepository
{
    Task<Email?> GetByAddress(string emailAddress, CancellationToken cancellationToken);
    Task<Email> CreateEmail(Email email, CancellationToken cancellationToken);
}