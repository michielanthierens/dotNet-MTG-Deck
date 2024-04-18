using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Howest.MagicCards.WebAPI.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void Deconstruct(this WebApplicationBuilder builder,
            out WebApplicationBuilder applicationBuilder,
            out IServiceCollection services,
            out ConfigurationManager configuration)
        {
            applicationBuilder = builder;
            services = builder.Services;
            configuration = builder.Configuration;
        }
    }
}
