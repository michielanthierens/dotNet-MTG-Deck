using Howest.MagicCards.Shared.DTO;
using Howest.MagicCards.Shared.Filters;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Text.Json;

namespace Howest.MagicCards.Web.Components.Pages;

public partial class FilterForm
{
    [Parameter]
    public string FormName { get; set; }

    [Parameter]
    public EventCallback<CardFilter> OnFilterChanged { get; set; }

    [Parameter]
    public CardFilter filter { get; set; }
    private IEnumerable<RarityDTO> rarities { get; set; } = new List<RarityDTO>();
    private string message { get; set; }
    private JsonSerializerOptions JsonOptions { get; }

    public FilterForm()
    {
        JsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
    }
    private async Task GetFilteredCards(EditContext context)
    {
        Console.WriteLine(filter);
        await OnFilterChanged.InvokeAsync(filter);
    }
    protected override async Task OnInitializedAsync()
    {
        HttpClient httpClient = httpClientFactory.CreateClient("WebAPI");
        httpClient.DefaultRequestHeaders.Add("api-version", "1.5");

        HttpResponseMessage response = await httpClient.GetAsync("Cards/rarities");

        string apiResponse = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            rarities = JsonSerializer.Deserialize<IEnumerable<RarityDTO>>(apiResponse, JsonOptions);
        }
        else
        {
            rarities = new List<RarityDTO>();
            message = $"Error: {response.ReasonPhrase}";
        }
    }
}
