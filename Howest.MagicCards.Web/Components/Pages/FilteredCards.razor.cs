using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.Shared.DTO;
using Howest.MagicCards.WebAPI.Wrappers;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace Howest.MagicCards.Web.Components.Pages
{
    public partial class FilteredCards
    {
        public IEnumerable<CardReadDTO> filteredCards { get; set; }
        public string message { get; set; }
        public string Name { get; set; }
        public string SetId { get; set; }
        public string ArtistName { get; set; }
        public string RarityCode { get; set; }
        public string Card_Type { get; set; }
        public string Card_Text { get; set; }
        public string MaxPageSize { get; set; }
        public string PageNumber { get; set; }
        public string PageSize { get; set; }
        public string Sort { get; set; }
        public JsonSerializerOptions JsonOptions { get; }

        public FilteredCards()
        {
            JsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }

        protected override async Task OnInitializedAsync()
        {
            HttpClient httpClient = httpClientFactory.CreateClient("WebAPI");
            HttpResponseMessage response = await httpClient.GetAsync($"Cards?" +
                                                                    $"Name={Name}&" +
                                                                    $"SetId={SetId}&" +
                                                                    $"ArtistName={ArtistName}&" +
                                                                    $"RarityCode={RarityCode}&" +
                                                                    $"Type={Card_Type}&" +
                                                                    $"Text={Card_Text}&" +
                                                                    $"MaxPageSize={MaxPageSize}&" +
                                                                    $"PageNumber={PageNumber}&" +
                                                                    $"PageSize={PageSize}&" +
                                                                    $"sort={Sort}");

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
}
