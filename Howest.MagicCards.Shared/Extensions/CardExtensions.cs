using Howest.MagicCards.DAL.Models;
using Microsoft.IdentityModel.Tokens;

namespace Howest.MagicCards.Shared.Extensions;

public static class CardExtensions
{
    public static IQueryable<Card> ToFilteredList(this IQueryable<Card> cards, string? name, string? set, string? artistName, string? rarityCode, string? type, string? text)
    {
        IQueryable<Card> filteredCards = cards;

        if (name != null)
        {
            filteredCards = filteredCards.Where(card => card.Name.Contains(name));
        }

        if (set != null)
        {
            filteredCards = filteredCards.Where(card => card.SetCodeNavigation.Name.Contains(set));
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

        filteredCards = filteredCards.Where(card => card.OriginalImageUrl != null);

        return filteredCards;
    }

    public static IQueryable<Card> SortOnCardName(this IQueryable<Card> cards, string orderByQueryString)
    {
        return orderByQueryString switch
        {
            "asc" => cards.OrderBy(card => card.Name),
            "desc" => cards.OrderByDescending(card => card.Name),
            _ => cards,
        };

    }

    public static IQueryable<Card> ToFilteredListGraphQL(this IQueryable<Card> cards, int? power, int? toughness)
{
    return cards.Where(c =>
        (power == null || c.Power == power.ToString()) &&
        (toughness == null || c.Toughness == toughness.ToString())
    );
}

}
