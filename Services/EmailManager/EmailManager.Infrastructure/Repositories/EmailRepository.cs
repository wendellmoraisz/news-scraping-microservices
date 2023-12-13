using EmailManager.Application.Repositories;
using EmailManager.Core.Entities;
using EmailManager.Infrastructure.Data;
using MongoDB.Driver;

namespace EmailManager.Infrastructure.Repositories;

public class EmailRepository : IEmailRepository
{
    private readonly IEmailContext _context;

    public EmailRepository(IEmailContext context)
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

    public async Task<List<Email>> GetAll(CancellationToken cancellationToken)
    {
        var emails = await _context
            .Emails
            .FindAsync(_ => true, cancellationToken: cancellationToken);
        
        return emails.ToList(cancellationToken: cancellationToken);
    }
}