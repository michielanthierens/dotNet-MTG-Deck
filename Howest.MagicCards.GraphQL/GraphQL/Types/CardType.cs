using GraphQL.Types;
using Howest.MagicCards.Shared.DTO;

namespace Howest.MagicCards.GraphQL.GraphQL.Types
{
    public class CardType : ObjectGraphType<CardReadDetailDTO>
    {
        public CardType() 
        {
            Field(c => c.Number);
            Field(c => c.CardName);
            Field(c => c.Power);
            Field(c => c.ConvertedManaCost);
            Field(c => c.Toughness);
            Field(c => c.Type);
            Field(c => c.Rarity);
            Field(c => c.Set);
            Field(c => c.Text);
            Field(c => c.ArtistName);
            Field(c => c.Flavor);
            Field(c => c.OriginalImageUrl);
            Field(c => c.Image);
            Field(c => c.OriginalType);
            Field<ListGraphType<StringGraphType>>("cardColors", resolve: context => context.Source.CardColors);
            Field<ListGraphType<StringGraphType>>("cardTypes", resolve: context => context.Source.CardTypes);
        }
    }
}
