using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Howest.MagicCards.DAL.DBContext;
using Howest.MagicCards.DAL.Repositories;
using Howest.MagicCards.GraphQL.Schemas;
using Howest.MagicCards.WebAPI.Extensions;
using Microsoft.EntityFrameworkCore;

var (builder, services, conf) = WebApplication.CreateBuilder(args);

services.AddDbContext<MtgContext>
    (options => options.UseSqlServer(conf.GetConnectionString("mtgDB")));
services.AddScoped<ICardRepository, SqlCardRepository>();
services.AddScoped<IArtistRepository, SqlArtistRepository>();

services.AddScoped<RootSchema>();
services.AddGraphQL()
    .AddGraphTypes(typeof(RootSchema), ServiceLifetime.Scoped)
    .AddDataLoader()
    .AddSystemTextJson();

var app = builder.Build();

app.UseGraphQL<RootSchema>();

app.UseGraphQLPlayground(
    "/ui/playground",
    new PlaygroundOptions()
    {
        EditorTheme = EditorTheme.Light
    });



app.Run();