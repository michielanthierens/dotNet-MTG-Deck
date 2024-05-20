using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.Shared.DTO;
using Howest.MagicCards.Shared.Filters;
using System.Net.Http;
using System.Text.Json;

namespace Howest.MagicCards.Web.Components.Pages
{
    public partial class FilterForm
    {
        public string message { get; set; }
                public JsonSerializerOptions JsonOptions { get; }

        public FilterForm()
        {
            JsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }


        private async Task GetFilteredCards()
        {                        

        }

        protected override async Task OnInitializedAsync()
        {
            HttpClient httpClient = httpClientFactory.CreateClient("WebAPI");
            httpClient.DefaultRequestHeaders.Add("api-version", "1.5");

            HttpResponseMessage response = await httpClient.GetAsync("Cards/rarities");

            string apiResponse = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                IEnumerable<RarityDTO> result = JsonSerializer.Deserialize<IEnumerable<RarityDTO>>(apiResponse, JsonOptions);

                rarities = result.ToList();
            } else
            {
                rarities = new List<RarityDTO>();

                message = $"Error: {response.ReasonPhrase}";
            }


        }
    }
}
