using GraphQL.Types;
using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.Shared.DTO;

namespace Howest.MagicCards.GraphQL.GraphQL.Types
{
    public class CardType : ObjectGraphType<Card>
    {
        public CardType()
        {
            Name = "Card";

            Field(c => c.Number, type: typeof(StringGraphType));
            Field(c => c.Name, type: typeof(StringGraphType));
            Field(c => c.Power, type: typeof(StringGraphType));
            Field(c => c.ConvertedManaCost, type: typeof(StringGraphType));
            Field(c => c.Toughness, type: typeof(StringGraphType));
            Field(c => c.Type, type: typeof(StringGraphType));
            Field(c => c.RarityCode , type: typeof(StringGraphType));
            Field(c => c.SetCode, type: typeof(StringGraphType));
            Field(c => c.Text, type: typeof(StringGraphType));
            // Field(c => c.ArtistId,); context
            Field(c => c.Flavor, type: typeof(StringGraphType));
            Field(c => c.OriginalImageUrl, type: typeof(StringGraphType));
            Field(c => c.Image, type: typeof(StringGraphType));
            Field(c => c.OriginalType, type: typeof(StringGraphType));
            // Field<ListGraphType<StringGraphType>>("cardColors", resolve: context => context.Source.CardColors);
            // Field<ListGraphType<StringGraphType>>("cardTypes", resolve: context => context.Source.CardTypes);
        }
    }
}
