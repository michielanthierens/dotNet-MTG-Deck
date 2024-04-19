using FluentValidation;
using FluentValidation.AspNetCore;
using Howest.MagicCards.Shared.FluentValidator;
using Howest.MagicCards.WebAPI.Extensions;

var (builder, services, conf) = WebApplication.CreateBuilder(args);

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddFluentValidationAutoValidation();

// services.AddValidatorsFromAssemblyContaining<CardCustomValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Hello world");

app.Run();

