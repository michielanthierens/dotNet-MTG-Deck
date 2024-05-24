using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Howest.MagicCards.DAL.Models;
public class DeckCard
{
    [BsonId]
    [BsonRequired]
    public string Id { get; set; }
    [BsonRequired]
    public string Name { get; set; }
    [BsonRequired]
    public int Amount { get; set; }
}
