using Howest.MagicCards.DAL.Models;

namespace Howest.MagicCards.Shared.Extensions;

public static class CardExtensions
{
    public static IQueryable<Card> ToFilteredList(this IQueryable<Card> cards, int setId, int ArtistId, int RaratyId, int CardTypeId, string CardText)
    {
        return null;
    }

    public static IQueryable<Card> ToFilteredListGraphQL(this IQueryable<Card> cards, int? power, int? toughness)
{
    return cards.Where(c =>
        (power == null || c.Power == power.ToString()) &&
        (toughness == null || c.Toughness == toughness.ToString())
    );
}

}
