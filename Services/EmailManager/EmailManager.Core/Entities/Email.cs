using MongoDB.Bson.Serialization.Attributes;

namespace EmailManager.Core.Entities;

public class Email : BaseEntity
{
    [BsonElement("Address")]
    public string Address { get; set; }
}