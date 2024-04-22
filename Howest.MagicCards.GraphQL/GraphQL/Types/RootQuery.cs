﻿using GraphQL;
using GraphQL.Types;
using Howest.MagicCards.DAL.Repositories;
using Howest.MagicCards.Shared.Extensions;

namespace Howest.MagicCards.GraphQL.GraphQL.Types
{
    public class RootQuery : ObjectGraphType
    {
        public RootQuery(ICardRepository cardRepo, IArtistRepository artistRepo)
        {
            Field<ListGraphType<CardType>>(
                "All Cards",
                Description = "Get all cards",
                arguments: new QueryArguments
                {
                    new QueryArgument<IntGraphType> { Name = "power", DefaultValue = null },
                    new QueryArgument<IntGraphType> { Name = "thoughness", DefaultValue = null }
                },
                resolve: context =>
                {
                    int? power = context.GetArgument<int?>("power");
                    int? toughness = context.GetArgument<int?>("toughness");
                    //todo return filtered list
                    return cardRepo.getAllCards().ToFilteredListGraphQL(power, toughness);
                });

            Field<ListGraphType<CardType>>(
                "All Artists",
                Description = "Get all artists",
                arguments: new QueryArguments
                {
                    new QueryArgument<IntGraphType> { Name = "limit", DefaultValue = 500 },
                },
                resolve: context =>
                {
                    int limit = context.GetArgument<int>("limit");
                    return artistRepo.GetAllArtists().Take(limit);
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
                    return artistRepo.GetArtistById(context.GetArgument<int>("artistId"));
                });
        }
    }
}
