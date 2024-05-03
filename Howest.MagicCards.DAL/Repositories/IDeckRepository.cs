using Howest.MagicCards.DAL.Models;

namespace Howest.MagicCards.DAL.Repositories
{
    public interface IDeckRepository
    {
        void addCardToDeck(string id, string name);
        Task clearDeck();
        Task<IEnumerable<DeckCard>> GetDeck();
        void removeCardFromDeck(string id);
    }
}