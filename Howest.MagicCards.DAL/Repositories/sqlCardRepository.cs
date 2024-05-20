using Howest.MagicCards.DAL.DBContext;
using Howest.MagicCards.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Howest.MagicCards.DAL.Repositories;

public class SqlCardRepository : ICardRepository
{
    private readonly MtgContext _db;

    public SqlCardRepository(MtgContext myMtgContext)
    {
        _db = myMtgContext;
    }

    public IQueryable<Card> getAllCards()
    {
        IQueryable<Card> AllCards = _db.Cards.Select(c => c);
        return AllCards;
    }

    public async Task<Card> GetCardbyId(int id)
    {
        Card foundCard = await _db.Cards.SingleOrDefaultAsync(c => c.Id == id);

        return foundCard;
    }


    public IQueryable<Card> getCardsByArtist(long? artistId)
    {    
        IQueryable<Card> CardsOfArtist = _db.Cards.Select(c => c).Where(c => c.ArtistId.Equals(artistId));
        return CardsOfArtist;
    }

    public IQueryable<Rarity> GetRarities()
    {
        IQueryable<Rarity> AllRarities = _db.Rarities.Select(c => c);
        return AllRarities;
    }
}
