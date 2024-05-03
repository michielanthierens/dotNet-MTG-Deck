using Howest.MagicCards.DAL.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Howest.MagicCards.DAL.Repositories;

public class DeckRepository : IDeckRepository
{
    private readonly string _connectionString;
    private readonly string _databaseName;
    private readonly string _deckCollection;
    public DeckRepository(ConfigurationManager conf)
    {
        _connectionString = conf.GetConnectionString(name: "mongoDB");
        _databaseName = conf.GetConnectionString(name: "database");
        _deckCollection = conf.GetConnectionString(name: "collection");
    }

    private IMongoCollection<T> ConnectToMongoDB<T>(in string collection)
    {
        MongoClient client = new MongoClient(_connectionString);
        IMongoDatabase db = client.GetDatabase(_databaseName);
        return db.GetCollection<T>(collection);
    }

    public async Task<IEnumerable<DeckCard>> GetDeck()
    {
        IMongoCollection<DeckCard> deckCollection = ConnectToMongoDB<DeckCard>(_deckCollection);
        IAsyncCursor<DeckCard> deckCards = await deckCollection.FindAsync(_ => true);

        return deckCards.ToEnumerable();
    }

    private DeckCard getCardOnId(string id)
    {
        IMongoCollection<DeckCard> deckCollection = ConnectToMongoDB<DeckCard>(_deckCollection);
        var card = deckCollection.Find(c => c.id.Equals(id));
        return (DeckCard)card;
    }

    public Task addCardToDeck(string id, string name)
    {
        IMongoCollection<DeckCard> deckCollection = ConnectToMongoDB<DeckCard>(_deckCollection);
        if (getCardOnId(id) is DeckCard foundCard)
        {
            changeAmount(foundCard, 1);
        }
        return deckCollection.InsertOneAsync(new DeckCard { id = id, name = name, amount = 0 });
    }

    public Task removeCardFromDeck(string number)
    {
        IMongoCollection<DeckCard> deckCollection = ConnectToMongoDB<DeckCard>(_deckCollection);
        try
        {
            DeckCard card = getCardOnId(number);
            if (card.amount > 1)
            {
                changeAmount(card, -1);
            }
            return deckCollection.DeleteOneAsync(c => c.id == number);
        }
        catch (Exception)
        {
            return null;
        }
    }
    private void changeAmount(DeckCard card, int amount)
    {
        IMongoCollection<DeckCard> deckCollection = ConnectToMongoDB<DeckCard>(_deckCollection);
        var filter = Builders<DeckCard>.Filter.Eq("id", card.id);
        card.amount += amount;
        deckCollection.ReplaceOneAsync(filter, card, new ReplaceOptions { IsUpsert = true });
    }

    public Task clearDeck()
    {
        IMongoCollection<DeckCard> deckCollection = ConnectToMongoDB<DeckCard>(_deckCollection);
        return deckCollection.DeleteManyAsync(_ => true);
    }
}