using Howest.MagicCards.DAL.Models;

namespace Howest.MagicCards.Shared.Extensions;

public static class CardExtensions
{
    public static IQueryable<Card> ToFilteredList(this IQueryable<Card> cards, string? name, string? set, string? artistName, string? rarityCode, string? type, string? text)
    {
        IQueryable<Card> filteredCards = cards;

        filteredCards = ApplyFilter(filteredCards, name != null, q => q.Where(card => card.Name.Contains(name)));
        filteredCards = ApplyFilter(filteredCards, set != null, q => q.Where(card => card.SetCodeNavigation.Name.Contains(set)));
        filteredCards = ApplyFilter(filteredCards, artistName != null, q => q.Where(card => card.Artist.FullName.Contains(artistName)));
        filteredCards = ApplyFilter(filteredCards, rarityCode != null, q => q.Where(card => card.RarityCode.Equals(rarityCode)));
        filteredCards = ApplyFilter(filteredCards, type != null, q => q.Where(card => card.Type.Contains(type)));
        filteredCards = ApplyFilter(filteredCards, text != null, q => q.Where(card => card.Text.Contains(text)));

        filteredCards = filteredCards.Where(card => card.OriginalImageUrl != null);

        return filteredCards;
    }

    public static IQueryable<Card> ApplyFilter(IQueryable<Card> query, bool condition, Func<IQueryable<Card>, IQueryable<Card>> filter)
    {
        return condition ? filter(query) : query;
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
