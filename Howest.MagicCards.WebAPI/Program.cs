
using Howest.MagicCards.DAL.DBContext;
using Howest.MagicCards.DAL.Repositories;
using Howest.MagicCards.Shared.Mappings;
using Howest.MagicCards.WebAPI.extensions;
using Howest.MagicCards.WebAPI.BehaviourConf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var (builder, services, conf) = WebApplication.CreateBuilder(args);

// Add services to the container.

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddResponseCaching();
services.AddSwaggerGen();

services.Configure<ApiBehaviourConf>(conf.GetSection("ApiSettings"));

services.AddDbContext<MtgContext>
    (options => options.UseSqlServer(conf.GetConnectionString("mtgDB")));
services.AddScoped<ICardRepository, SqlCardRepository>();

services.AddAutoMapper(new Type[]
{
    typeof(CardsProfile)
});


services.AddApiVersioning(o =>
{
    o.ReportApiVersions = true;
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new ApiVersion(1, 1);
});



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
