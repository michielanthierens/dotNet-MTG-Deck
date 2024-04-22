using GraphQL.Types;
using Howest.MagicCards.Shared.DTO;

namespace Howest.MagicCards.GraphQL.GraphQL.Types
{
    public class CardType : ObjectGraphType<CardReadDetailDTO>
    {
        public CardType()
        {
            Name = "Card";

            Field(c => c.Number, type: typeof(StringGraphType));
            Field(c => c.CardName, type: typeof(StringGraphType));
            Field(c => c.Power, type: typeof(StringGraphType));
            Field(c => c.ConvertedManaCost, type: typeof(StringGraphType));
            Field(c => c.Toughness, type: typeof(StringGraphType));
            Field(c => c.Type, type: typeof(StringGraphType));
            Field(c => c.Rarity, type: typeof(StringGraphType));
            Field(c => c.Set, type: typeof(StringGraphType));
            Field(c => c.Text, type: typeof(StringGraphType));
            Field(c => c.ArtistName, type: typeof(StringGraphType));
            Field(c => c.Flavor, type: typeof(StringGraphType));
            Field(c => c.OriginalImageUrl, type: typeof(StringGraphType));
            Field(c => c.Image, type: typeof(StringGraphType));
            Field(c => c.OriginalType, type: typeof(StringGraphType));
            Field<ListGraphType<StringGraphType>>("cardColors", resolve: context => context.Source.CardColors);
            Field<ListGraphType<StringGraphType>>("cardTypes", resolve: context => context.Source.CardTypes);
        }
    }
}
