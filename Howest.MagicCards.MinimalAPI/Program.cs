using FluentValidation;
using FluentValidation.AspNetCore;
using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.DAL.Repositories;
using Howest.MagicCards.MinimalAPI.extensions;
using Howest.MagicCards.Shared.FluentValidator;
using Howest.MagicCards.WebAPI.BehaviourConf;
using Howest.MagicCards.WebAPI.Extensions;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

var (builder, services, conf) = WebApplication.CreateBuilder(args);

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddFluentValidationAutoValidation();

services.AddSingleton<IDeckRepository, DeckRepository>();
services.AddValidatorsFromAssemblyContaining<CardCustomValidator>();
//todo change to own apibehaviourconf
services.Configure<ApiBehaviourConf>(conf.GetSection("ApiSettings"));

#region testingMongoDB
//string connectionstring = conf.GetConnectionString(name: "mongoDB");
//string databaseName = conf.GetConnectionString(name: "database");
//string collectionName = conf.GetConnectionString(name: "collection");

//var client = new MongoClient(connectionstring);
//var db = client.GetDatabase(databaseName);
//var collection = db.GetCollection<DeckCard>(collectionName);

//var card = new DeckCard { id = "2", name = "swords", amount = 12 };

//await collection.InsertOneAsync(card);

//var result = await collection.FindAsync(_ => true);

//foreach (var res in result.ToList())
//{
//    Console.WriteLine($"{res.id}: {res.name}, {res.amount}");
//}
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

RouteGroupBuilder deckGroup = app.MapGroup($"deck").WithTags("deck");

deckGroup.MapDeckEndpoints();

app.Run();

