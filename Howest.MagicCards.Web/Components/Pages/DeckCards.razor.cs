using Howest.MagicCards.DAL.Models;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace Howest.MagicCards.Web.Components.Pages
{
    public partial class DeckCards
    {
        [Parameter]
        public IEnumerable<DeckCard> deckCards { get; set; }
        [Parameter]
        public string message { get; set; }
        private JsonSerializerOptions JsonOptions { get; }

        public DeckCards()
        {
            JsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }

        protected override async Task OnInitializedAsync()
        {
            await getDeck();
        }

        public async Task getDeck()
        {
            HttpClient httpClient = httpClientFactory.CreateClient("MinimalAPI");

            HttpResponseMessage response = await httpClient.GetAsync("");

            string apiResponse = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                deckCards = JsonSerializer.Deserialize<IEnumerable<DeckCard>>(apiResponse, JsonOptions);
            }
            else
            {
                message = $"Error: {response.ReasonPhrase}";
            }
        }

        async Task removeCardFromDeck(DeckCard card)
        {
            HttpClient httpClient = httpClientFactory.CreateClient("MinimalAPI");
            var content = new StringContent(string.Empty);

            HttpResponseMessage response = await httpClient.PutAsync($"remove?id={card.id}&name={card.name}", content);

            if (response.IsSuccessStatusCode)
            {
                await getDeck();
                StateHasChanged();
            }
            else
            {
                message = $"Error: {response.ReasonPhrase}";
            }
        }

        public async Task AddCardToDeck(DeckCard card)
        {
            HttpClient httpClient = httpClientFactory.CreateClient("MinimalAPI");
            var content = new StringContent(string.Empty);

            HttpResponseMessage response = await httpClient.PutAsync($"add?id={card.id}&name={card.name}", content);

            if (response.IsSuccessStatusCode)
            {
                await getDeck();
                StateHasChanged();
            }
            else
            {
                message = $"Error: {response.ReasonPhrase}";
            }
        }

        private async Task ClearDeck()
        {
            HttpClient httpClient = httpClientFactory.CreateClient("MinimalAPI");
            var content = new StringContent(string.Empty);

            HttpResponseMessage response = await httpClient.DeleteAsync($"clear");

            if (response.IsSuccessStatusCode)
            {
                await getDeck();
            }
            else
            {
                message = $"Error: {response.ReasonPhrase}";
            }
        }
    }
}
