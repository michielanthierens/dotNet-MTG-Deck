using FluentValidation.AspNetCore;
using Howest.MagicCards.WebAPI.Extensions;
using MongoDB.Driver;
using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using Howest.MagicCards.WebAPI.BehaviourConf;
using Amazon.Runtime.Internal.Util;

var (builder, services, conf) = WebApplication.CreateBuilder(args);

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddFluentValidationAutoValidation();

// services.AddValidatorsFromAssemblyContaining<CardCustomValidator>();

services.Configure<ApiBehaviourConf>(conf.GetSection("ApiSettings"));
string connectionstring = conf.GetConnectionString(name: "mongoDB");
string databaseName = conf.GetConnectionString(name: "database");
string collectionName = conf.GetConnectionString(name: "collection");

var client = new MongoClient(connectionstring);
var db = client.GetDatabase(databaseName);
var collection = db.GetCollection<Deck>(collectionName);

var card = new Deck { id = "1", name = "daggers", amount = 1 };

await collection.InsertOneAsync(card);

var result = await collection.FindAsync(_ => true);

foreach (var res in result.ToList())
{
    Console.WriteLine($"{res.id}: {res.name}, {res.amount}");
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Hello world");

app.Run();

