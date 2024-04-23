using Howest.MagicCards.DAL.Models;

namespace Howest.MagicCards.Shared.Extensions;

public static class CardExtensions
{
    public static IQueryable<Card> ToFilteredList(this IQueryable<Card> cards, string? name, string? set, string? artist, string? rarity, string? type, string? text)
    {
        return cards.Where(card =>
                card.Name.Equals(name)); //&&
                //card.SetCode.Equals(set) &&
                //card.Artist.FullName.Equals(artist) &&
                //card.RarityCode.Equals(rarity) &&
                //card.Type.Equals(type) &&
                //card.Text.Equals(text));
    }
}
