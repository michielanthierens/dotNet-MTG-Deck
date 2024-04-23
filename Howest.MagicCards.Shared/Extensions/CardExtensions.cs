﻿using Howest.MagicCards.DAL.Models;

namespace Howest.MagicCards.Shared.Extensions;

public static class CardExtensions
{
    public static IQueryable<Card> ToFilteredList(this IQueryable<Card> cards, string? name, string? set, string? artist, string? rarity, string? type, string? text)
    {
        IQueryable<Card> filteredCards = cards;

        if (name != null)
        {
            filteredCards = filteredCards.Where(card => card.Name.Equals(name));
        }

        if (set != null)
        {
            filteredCards = filteredCards.Where(card => card.SetCode.Equals(set));
        }

        if (artist != null)
        {
            filteredCards = filteredCards.Where(card => card.Artist.FullName.Equals(artist));
        }

        if (rarity != null)
        {
            filteredCards = filteredCards.Where(card => card.RarityCode.Equals(rarity));
        }

        if (type != null)
        {
            filteredCards = filteredCards.Where(card => card.Type.Equals(type));
        }

        if (text != null)
        {
            filteredCards = filteredCards.Where(card => card.Text.Equals(text));
        }

        return filteredCards;
    }
}
