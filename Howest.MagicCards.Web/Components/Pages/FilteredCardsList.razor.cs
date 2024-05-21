using Howest.MagicCards.Shared.DTO;
using Microsoft.AspNetCore.Components;
using System.Text.Json;


namespace Howest.MagicCards.Web.Components.Pages
{
    public partial class FilteredCardsList
    {
        [Parameter]
        public IEnumerable<CardReadDTO> Cards { get; set; }
        private CardReadDetailDTO _hoveredCard;

        public string message { get; set; }

        private async Task ShowCardInfo(string cardNumber)
        {
            HttpClient httpClient = httpClientFactory.CreateClient("WebAPI");
            httpClient.DefaultRequestHeaders.Add("api-version", "1.5");

            HttpResponseMessage response = await httpClient.GetAsync($"Cards/{cardNumber}");

            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                _hoveredCard = JsonSerializer.Deserialize<CardReadDetailDTO>(apiResponse);
            }
            else
            {
                message = $"Error: {response.ReasonPhrase}";
            }
        }

        private void HideCardInfo()
        {
            _hoveredCard = null;
            message = string.Empty;
        }

        public async Task AddCardToDeck(CardReadDTO card)
        {
            HttpClient httpClient = httpClientFactory.CreateClient("MinimalAPI");
            var content = new StringContent(string.Empty);

            HttpResponseMessage response = await httpClient.PutAsync($"add?id={card.Number}_{card.Name}&name={card.Name}", content);

            if (response.IsSuccessStatusCode)
            {
                // todo refresh deck
            }
            else
            {
                message = $"Error: {response.ReasonPhrase}";
            }
        }
    }
}
