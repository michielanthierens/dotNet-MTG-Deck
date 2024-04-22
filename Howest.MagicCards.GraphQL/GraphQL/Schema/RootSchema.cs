using GraphQL.Types;
using Howest.MagicCards.GraphQL.GraphQL.Types;

namespace Howest.MagicCards.GraphQL.Schemas
{
    public class RootSchema : Schema
    {
        public RootSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<RootQuery>();
        }
    }
}
