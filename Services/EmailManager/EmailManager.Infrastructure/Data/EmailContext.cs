using EmailManager.Core.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace EmailManager.Infrastructure.Data;

public class EmailContext : IEmailContext
{
    public IMongoCollection<Email> Emails { get; set; }

    public EmailContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
        Emails = database.GetCollection<Email>(configuration.GetValue<string>("DatabaseSettings:EmailsCollection"));
    }
}