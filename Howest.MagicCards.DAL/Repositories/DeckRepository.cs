using Howest.MagicCards.DAL.Models;
using Microsoft.AspNetCore.Http.HttpResults;
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

    private async Task<IEnumerable<DeckCard>> GetDeck()
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

    public Task addCardToDeck(DeckCard card)
    {
        // check if < 60
        // check if exsist
        IMongoCollection<DeckCard> deckCollection = ConnectToMongoDB<DeckCard>(_deckCollection);
        if (getCardOnId(card.id) != null) 
        {
            changeAmount(card, 1);
        }
        return deckCollection.InsertOneAsync(card);
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
        } catch (Exception)
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
