using Howest.MagicCards.DAL.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Howest.MagicCards.DAL.Repositories;

public class DeckRepository : IDeckRepository
{
    private readonly string _connectionString;
    private readonly string _databaseName;
    private readonly string _deckCollection;
    private readonly int _maxDeckSize;
    public DeckRepository(IConfiguration conf)
    {
        _connectionString = conf.GetConnectionString(name: "mongoDB");
        _databaseName = conf.GetConnectionString(name: "database");
        _deckCollection = conf.GetConnectionString(name: "collection");
        _maxDeckSize = int.Parse(conf["MaxDeckSize"]);
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

    public async Task addCardToDeckAsync(string id, string name)
    {
        IMongoCollection<DeckCard> deckCollection = ConnectToMongoDB<DeckCard>(_deckCollection);

        if (await checkDeckSizeAsync())
        {
            DeckCard foundCard = deckCollection.Find(card => card.Id == id).FirstOrDefault();

            if (foundCard != null)
            {
                foundCard.Amount++;
                FilterDefinition<DeckCard> filter = Builders<DeckCard>.Filter.Eq(c => c.Id, id);
                UpdateDefinition<DeckCard> update = Builders<DeckCard>.Update.Set(c => c.Amount, foundCard.Amount);
                deckCollection.UpdateOne(filter, update);
            }
            else
            {
                deckCollection.InsertOne(new DeckCard() { Id = id, Name = name, Amount = 1 });
            }
        }
    }

    public void removeCardFromDeck(string id)
    {
        IMongoCollection<DeckCard> deckCollection = ConnectToMongoDB<DeckCard>(_deckCollection);

        DeckCard foundCard = deckCollection.Find(card => card.Id == id).FirstOrDefault();

        if (foundCard != null)
        {
            if (foundCard.Amount > 1)
            {
                foundCard.Amount--;
                FilterDefinition<DeckCard> filter = Builders<DeckCard>.Filter.Eq(c => c.Id, id);
                UpdateDefinition<DeckCard> update = Builders<DeckCard>.Update.Set(c => c.Amount, foundCard.Amount);
                deckCollection.UpdateOne(filter, update);
            }
            else
            {
                deckCollection.DeleteOne(card => card.Id == id);
            }
        }
    }

    public Task clearDeck()
    {
        IMongoCollection<DeckCard> deckCollection = ConnectToMongoDB<DeckCard>(_deckCollection);
        return deckCollection.DeleteManyAsync(_ => true);
    }

    private async Task<bool> checkDeckSizeAsync()
    {
        IEnumerable<DeckCard> cards = await GetDeck();
        int totalAmount = cards.Sum(Card => Card.Amount);

        if (totalAmount < _maxDeckSize) {
            return true;        
        }
        return false;

    }
}