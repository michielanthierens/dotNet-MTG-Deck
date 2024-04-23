using Howest.MagicCards.DAL.Models;

namespace Howest.MagicCards.DAL.Repositories
{
    public interface ICardRepository
    {
        IQueryable<Card> getAllCards();
        IQueryable<Card> getCardsByArtist(long? artistId);
    }
}