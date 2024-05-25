using Howest.MagicCards.Web.Components;
using Howest.MagicCards.Shared.Extensions;

(WebApplicationBuilder builder, IServiceCollection services, ConfigurationManager conf) = WebApplication.CreateBuilder(args);

services.AddRazorComponents()
    .AddInteractiveServerComponents();

services.AddHttpClient("WebAPI", client =>
{
    client.BaseAddress = new Uri(conf.GetConnectionString("WebAPI"));
});

services.AddHttpClient("MinimalAPI", client =>
{
    client.BaseAddress = new Uri(conf.GetConnectionString("MinimalApi"));
});

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
