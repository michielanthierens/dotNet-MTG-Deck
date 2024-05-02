
using Howest.MagicCards.DAL.DBContext;
using Howest.MagicCards.DAL.Repositories;
using Howest.MagicCards.Shared.Mappings;
using Howest.MagicCards.WebAPI.BehaviourConf;
using Howest.MagicCards.WebAPI.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var (builder, services, conf) = WebApplication.CreateBuilder(args);

// Add services to the container.

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddMemoryCache();
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1.1", new OpenApiInfo
    {
        Title = "Books API version 1.1",
        Version = "v1.1",
        Description = "API to return cards"
    });
    c.SwaggerDoc("v1.5", new OpenApiInfo
    {
        Title = "Books API version 1.5",
        Version = "v1.5",
        Description = "API to return cards"
    });
});

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
o.ApiVersionReader = ApiVersionReader.Combine(
    new HeaderApiVersionReader("api-version"),
    new MediaTypeApiVersionReader("v"));
});

builder.Services.AddVersionedApiExplorer(
    options =>
    {
        // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
        // note: the specified format code will format the version as "'v'major[.minor][-status]"
        options.GroupNameFormat = "'v'VVV";

        // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
        // can also be used to control the format of the API version in route templates
        options.SubstituteApiVersionInUrl = true;
    }
);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1.1/swagger.json", "CardsAPI v1.1");
        c.SwaggerEndpoint("/swagger/v1.5/swagger.json", "CardsAPI v1.5");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
