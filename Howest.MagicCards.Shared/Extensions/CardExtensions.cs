using Howest.MagicCards.DAL.Models;

namespace Howest.MagicCards.Shared.Extensions;

public static class CardExtensions
{
    public static IQueryable<Card> ToFilteredList(this IQueryable<Card> cards, string? name, string? setId, string? artistName, string? rarityCode, string? type, string? text)
    {
        IQueryable<Card> filteredCards = cards;

        if (name != null)
        {
            filteredCards = filteredCards.Where(card => card.Name.Contains(name));
        }

        if (setId != null)
        {
            filteredCards = filteredCards.Where(card => card.SetCode.Equals(setId));
        }

        if (artistName != null)
        {
            filteredCards = filteredCards.Where(card => card.Artist.FullName.Contains(artistName));
        }

        if (rarityCode != null)
        {
            filteredCards = filteredCards.Where(card => card.RarityCode.Equals(rarityCode));
        }

        if (type != null)
        {
            filteredCards = filteredCards.Where(card =>
                card.Type.Contains(type));
        }

        if (text != null)
        {
            filteredCards = filteredCards.Where(card => card.Text.Contains(text));
        }

        return filteredCards;
    }
}
