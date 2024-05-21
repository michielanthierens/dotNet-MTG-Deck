using Howest.MagicCards.Shared.DTO;
using Howest.MagicCards.Shared.Filters;
using Howest.MagicCards.WebAPI.Wrappers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Json;


namespace Howest.MagicCards.Web.Components.Pages;

public partial class FilteredCards
{
    [Parameter]
    public CardFilter Filter { get; set; }
    private IEnumerable<CardReadDTO> filteredCards { get; set; }
    private string message { get; set; } = string.Empty;
    private JsonSerializerOptions JsonOptions { get; }

    public FilteredCards()
    {
        JsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
    }

    protected override async Task OnParametersSetAsync()
    {
        await LoadFilteredCards();
    }

    private async Task LoadFilteredCards()
    {
        HttpClient httpClient = httpClientFactory.CreateClient("WebAPI");
        httpClient.DefaultRequestHeaders.Add("api-version", "1.5");

        var queryParams = new Dictionary<string, string>();

        if (!string.IsNullOrEmpty(Filter.Name))
            queryParams.Add("Name", Filter.Name);
        if (!string.IsNullOrEmpty(Filter.SetId))
            queryParams.Add("SetId", Filter.SetId);
        if (!string.IsNullOrEmpty(Filter.ArtistName))
            queryParams.Add("ArtistName", Filter.ArtistName);
        if (!string.IsNullOrEmpty(Filter.RarityCode))
            queryParams.Add("RarityCode", Filter.RarityCode);
        if (!string.IsNullOrEmpty(Filter.Type))
            queryParams.Add("Type", Filter.Type);
        if (!string.IsNullOrEmpty(Filter.Text))
            queryParams.Add("Text", Filter.Text);
        if (Filter.PageNumber > 0)
            queryParams.Add("PageNumber", Filter.PageNumber.ToString());
        if (Filter.PageSize > 0)
            queryParams.Add("PageSize", Filter.PageSize.ToString());
        if (!string.IsNullOrEmpty(Filter.Sort))
            queryParams.Add("Sort", Filter.Sort);

        string url = QueryHelpers.AddQueryString("Cards", queryParams);

        HttpResponseMessage response = await httpClient.GetAsync(url);

        string apiResponse = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            PagedResponse<IEnumerable<CardReadDTO>> result = JsonSerializer.Deserialize<PagedResponse<IEnumerable<CardReadDTO>>>(apiResponse, JsonOptions);
            filteredCards = result.Data;
        }
        else
        {
            filteredCards = new List<CardReadDTO>();
            message = $"Error: {response.ReasonPhrase}";
        }
    }
}
