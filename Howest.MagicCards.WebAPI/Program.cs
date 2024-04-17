
using Howest.MagicCards.WebAPI.extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Howest.MagicCards.DAL.DBContext;
using Howest.MagicCards.DAL.Repositories;

var (builder, services, conf) = WebApplication.CreateBuilder(args);

// Add services to the container.

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddResponseCaching();
services.AddSwaggerGen();

services.AddDbContext<MtgContext>
    (options => options.UseSqlServer(conf.GetConnectionString("mtgDB")));
services.AddScoped<ICardRepository, SqlCardRepository>();

//todo services.AddAutoMapper(new Type[] { typeof(Shared.Mappings.MtgProfile) });

//todo services.AddApiVersioning();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
