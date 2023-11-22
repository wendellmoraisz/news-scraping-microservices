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
    
    public async Task<Email?> GetByAddress(string emailAddress)
    {
        return await _context
            .Emails
            .Find(email => email.Address == emailAddress)
            .FirstOrDefaultAsync();
    }
}