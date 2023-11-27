using EmailManager.Core.Entities;
using MongoDB.Driver;

namespace EmailManager.Infrastructure.Data;

public interface IEmailContext
{
    IMongoCollection<Email> Emails { get; set; }
}