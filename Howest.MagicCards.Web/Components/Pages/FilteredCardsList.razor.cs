using Howest.MagicCards.Shared.DTO;
using Howest.MagicCards.WebAPI.Wrappers;
using Microsoft.AspNetCore.Components;
using System.Text;
using System.Text.Json;


namespace Howest.MagicCards.Web.Components.Pages
{
    public partial class FilteredCardsList
    {
        [Parameter]
        public IEnumerable<CardReadDTO> Cards { get; set; }

        [Parameter]
        public EventCallback GetDeck { get; set; }

        private CardReadDetailDTO _hoveredCard = new ();
        public string message { get; set; }

        private JsonSerializerOptions JsonOptions { get; }

        public FilteredCardsList()
        {
            JsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }

        private async Task ShowCardInfo(string cardNumber)
        {
            HttpClient httpClient = httpClientFactory.CreateClient("WebAPI");
            httpClient.DefaultRequestHeaders.Add("api-version", "1.5");

            HttpResponseMessage response = await httpClient.GetAsync($"Cards/{cardNumber}");


            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                Response<CardReadDetailDTO> result = JsonSerializer.Deserialize<Response<CardReadDetailDTO>>(apiResponse, JsonOptions);

                _hoveredCard = result.Data;
            }
            else
            {
                message = $"Error: {response.ReasonPhrase}";
            }
        }

        public async Task AddCardToDeck(CardReadDTO card)
        {
            HttpClient httpClient = httpClientFactory.CreateClient("MinimalAPI");
            StringContent content = new StringContent(string.Empty);

            DeckPutDTO updateCard = new DeckPutDTO { Id = card.MtgId, Name = card.Name };

            string json = JsonSerializer.Serialize(updateCard);

            content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PutAsync($"add", content);

            if (response.IsSuccessStatusCode)
            {
                await GetDeck.InvokeAsync();
            }
            else
            {
                message = $"Error: {response.ReasonPhrase}";
            }

            await GetDeck.InvokeAsync();
        }
    }
}
