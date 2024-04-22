using GraphQL.Types;
using Howest.MagicCards.DAL.Repositories;

namespace Howest.MagicCards.GraphQL.GraphQL.Types
{
    public class RootQuery : ObjectGraphType
    {
        public RootQuery(ICardRepository cardRepo)
        {
            Field<ListGraphType<CardType>>(
                "cards",
                Description = "Get all cards",
                resolve: context =>
                {
                    return cardRepo.getAllCards();
                });
        }
    }
}
