using GraphQL.Types;
using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.DAL.Repositories;

namespace Howest.MagicCards.GraphQL.GraphQL.Types
{
    public class CardType : ObjectGraphType<Card>
    {
        public CardType(IArtistRepository artistRepository)
        {
            Name = "Card";

            Field(c => c.Number, type: typeof(StringGraphType));
            Field(c => c.Name, type: typeof(StringGraphType));
            Field(c => c.Power, type: typeof(StringGraphType));
            Field(c => c.ConvertedManaCost, type: typeof(StringGraphType));
            Field(c => c.Toughness, type: typeof(StringGraphType));
            Field(c => c.Type, type: typeof(StringGraphType));
            Field(c => c.Text, type: typeof(StringGraphType));
            Field<ArtistType>(
                "Artist",
                resolve: context => artistRepository.GetArtistById((int)(context.Source.ArtistId ?? default))
                );
        }
    }
}
