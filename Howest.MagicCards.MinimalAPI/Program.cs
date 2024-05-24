using FluentValidation;
using FluentValidation.AspNetCore;
using Howest.MagicCards.DAL.Repositories;
using Howest.MagicCards.MinimalAPI.extensions;
using Howest.MagicCards.Shared.FluentValidator;
using Howest.MagicCards.Shared.Mappings;
using Howest.MagicCards.WebAPI.extensions;

var (builder, services, conf) = WebApplication.CreateBuilder(args);

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddFluentValidationAutoValidation();

services.AddSingleton<IDeckRepository, DeckRepository>();
services.AddValidatorsFromAssemblyContaining<CardCustomValidator>();
services.AddAutoMapper(typeof(CardsProfile));
//todo change to own apibehaviourconf

WebApplication app = builder.Build();   

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

RouteGroupBuilder deckGroup = app.MapGroup($"deck").WithTags("deck");

deckGroup.MapDeckEndpoints();

app.Run();

