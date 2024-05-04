using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Howest.MagicCards.DAL.Models;
public class DeckCard
{
    [BsonId]
    [BsonRequired]
    public string id { get; set; }
    [BsonRequired]
    public string name { get; set; }
    [BsonRequired]
    public int amount { get; set; }
}
