using EmailManager.Application.Repositories;
using EmailManager.Core.Entities;
using EmailManager.Infrastructure.Data;
using MongoDB.Driver;

namespace EmailManager.Infrastructure.Repositories;

public class EmailRepository : IEmailRepository
{
    private readonly EmailContext _context;

    public EmailRepository(EmailContext context)
    {
        _context = context;
    }

    public async Task<Email?> GetByAddress(string emailAddress, CancellationToken cancellationToken)
    {
        return await _context
            .Emails
            .Find(email => email.Address == emailAddress)
            .FirstOrDefaultAsync(cancellationToken);
    }
    
    public async Task<Email> CreateEmail(Email email, CancellationToken cancellationToken)
    {
        await _context.Emails.InsertOneAsync(email, cancellationToken);
        return email;
    }
    
}