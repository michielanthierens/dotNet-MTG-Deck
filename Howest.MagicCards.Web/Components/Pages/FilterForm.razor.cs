using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.Shared.DTO;
using Howest.MagicCards.Shared.Filters;
using System.Net.Http;
using System.Text.Json;

namespace Howest.MagicCards.Web.Components.Pages
{
    public partial class FilterForm
    {
        private CardFilter filter { get; set; } = new();
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

        private async Task GetFilteredCards()
        {
            // Implement the method to get filtered cards based on the filter criteria.
        }
    }
}
