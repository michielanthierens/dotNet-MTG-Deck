using Howest.MagicCards.DAL.Models;

namespace Howest.MagicCards.DAL.Repositories
{
    public interface IDeckRepository
    {
        Task addCardToDeck(string id, string name);
        Task clearDeck();
        Task<IEnumerable<DeckCard>> GetDeck();
        Task removeCardFromDeck(string number);
    }
}