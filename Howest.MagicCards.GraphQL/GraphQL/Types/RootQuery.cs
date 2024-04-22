using GraphQL;
using GraphQL.Types;
using Howest.MagicCards.DAL.Repositories;

namespace Howest.MagicCards.GraphQL.GraphQL.Types
{
    public class RootQuery : ObjectGraphType
    {
        public RootQuery(ICardRepository cardRepo)
        {
            Field<ListGraphType<CardType>>(
                "All Cards",
                Description = "Get all cards",
                resolve: context =>
                {
                    return cardRepo.getAllCards();
                });

            Field<ListGraphType<CardType>>(
                "All Artists",
                Description = "Get all artists",
                resolve: context =>
                {
                    return cardRepo.getAllArtists();
                });

            Field<ListGraphType<CardType>>(
                "Artists",
                Description = "Get artist",
                arguments: new QueryArguments
                {
                    new QueryArgument<IntGraphType> { Name = "artistId"}
                },
                resolve: context =>
                {
                    return cardRepo.GetArtist(context.GetArgument<int>("artistId"));
                });
        }
    }
}
