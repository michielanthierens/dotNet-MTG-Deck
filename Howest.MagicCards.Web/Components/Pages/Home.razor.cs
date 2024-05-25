using System.Text.Json;
using Howest.MagicCards.Shared.DTO;
using Howest.MagicCards.WebAPI.Wrappers;

namespace Howest.MagicCards.Web.Components.Pages;

public partial class Home
{
    private IEnumerable<DeckReadDTO> DeckCards { get; set; }
    private JsonSerializerOptions JsonOptions { get; }
    private string _message;

    public Home()
    {
        JsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
    }

    private async Task GetDeck()
    {
        HttpClient httpClient = httpClientFactory.CreateClient("MinimalAPI");

        HttpResponseMessage response = await httpClient.GetAsync("");

        string apiResponse = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            Response<IEnumerable<DeckReadDTO>> apiResponseObj = JsonSerializer.Deserialize<Response<IEnumerable<DeckReadDTO>>>(apiResponse, JsonOptions);
            if (apiResponseObj is { Succeeded: true })
            {
                DeckCards = apiResponseObj.Data ?? new List<DeckReadDTO>();
                _message = null;
            }
            else
            {
                _message = apiResponseObj?.Message;
            }
        }
        else
        {
            _message = $"Error: {response.ReasonPhrase}";
        }
    }
}