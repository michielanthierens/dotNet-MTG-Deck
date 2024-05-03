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
        var client = new MongoClient(_connectionString);
        var db = client.GetDatabase(_databaseName);
        return db.GetCollection<T>(collection);
    }

    private async Task<IEnumerable<DeckCard>> GetDeck()
    {
        var deckCollection = ConnectToMongoDB<DeckCard>(_deckCollection);
        IAsyncCursor<DeckCard> deckCards = await deckCollection.FindAsync(_ => true);
        
        return deckCards.ToEnumerable();
    }

    public void addCardToDeck(DeckCard card)
    {
        // check if < 60
    }

    public void removeCardFromDeck(string number)
    {

    }

    public void increaseAmount()
    {
        // check if < 60

    }

    public void clearDeck()
    {

    }



}
