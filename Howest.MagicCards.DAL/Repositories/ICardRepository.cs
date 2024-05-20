using Howest.MagicCards.DAL.Models;

namespace Howest.MagicCards.DAL.Repositories
{
    public interface ICardRepository
    {
        IQueryable<Card> getAllCards();
        Task<Card> GetCardbyId(int id);
        IQueryable<Card> getCardsByArtist(long? artistId);
        IQueryable<Rarity> GetRarities();
    }
}