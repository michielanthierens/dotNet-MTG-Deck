using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Howest.MagicCards.DAL.Models;
public class DeckCard
{
    [BsonId]
    public string id { get; set; }
    public string name { get; set; }
    public int amount { get; set; }
}
