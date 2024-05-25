using Howest.MagicCards.Shared.DTO;
using Howest.MagicCards.Shared.Filters;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace Howest.MagicCards.Web.Components.Pages
{
    public partial class FilteredCardsForm
    {
        [Parameter]
        public BlazorFilter BlazorFilter { get; set; }

        [Parameter]
        public string Message { get; set; }

        [Parameter]
        public EventCallback LoadFilteredCards { get; set; }

        private IEnumerable<RarityDTO> Rarities { get; set; } = new List<RarityDTO>();
        private JsonSerializerOptions JsonOptions { get; }

        public FilteredCardsForm()
        {
            JsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }

        protected override async Task OnInitializedAsync()
        {
            GetRarities();
        }

        private async void GetRarities()
        {
            HttpClient httpClient = httpClientFactory.CreateClient("WebAPI");
            httpClient.DefaultRequestHeaders.Add("api-version", "1.5");
            // double check
            HttpResponseMessage response = await httpClient.GetAsync("Cards/rarities");

            string apiResponse = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Rarities = JsonSerializer.Deserialize<IEnumerable<RarityDTO>>(apiResponse, JsonOptions);
            }
            else
            {
                Rarities = new List<RarityDTO>();
                Message = $"Error: {response.ReasonPhrase}";
            }
        }
    }
}
