using GraphQL.Types;
using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.DAL.Repositories;
using Howest.MagicCards.Shared.DTO;

namespace Howest.MagicCards.GraphQL.GraphQL.Types
{
    public class ArtistType: ObjectGraphType<Artist>
    {
        public ArtistType(ICardRepository cardRepository)
        {
            Name = "Artist";

            Field(a => a.FullName, type: typeof(StringGraphType));
            Field<ListGraphType<CardType>>(
                "cards",
                resolve: context => cardRepository.getCardsByArtist(context.Source.Id)
            );
        }
    }
}
