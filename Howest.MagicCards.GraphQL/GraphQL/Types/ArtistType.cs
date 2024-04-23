using GraphQL.Types;
using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.Shared.DTO;

namespace Howest.MagicCards.GraphQL.GraphQL.Types
{
    public class ArtistType: ObjectGraphType<Artist>
    {
        public ArtistType()
        {
            Name = "Artist";

            Field(a => a.FullName, type: typeof(StringGraphType));
            Field<ListGraphType<CardType>>(
                "cards",
                resolve: context => context.Source.Cards
            );
        }
    }
}
