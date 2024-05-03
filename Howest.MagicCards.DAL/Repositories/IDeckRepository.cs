using Howest.MagicCards.DAL.Models;

namespace Howest.MagicCards.DAL.Repositories
{
    public interface IDeckRepository
    {
        void addCardToDeck(DeckCard card);
        void clearDeck();
        void increaseAmount();
        void removeCardFromDeck(string number);
    }
}