using Howest.MagicCards.Shared.DTO;
using Howest.MagicCards.WebAPI.Wrappers;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Json;


namespace Howest.MagicCards.Web.Components.Pages
{
    public partial class FilteredCards
    {
        public IEnumerable<CardReadDTO> filteredCards { get; set; }
        public string message { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string SetId { get; set; } = string.Empty;
        public string ArtistName { get; set; } = string.Empty;
        public string RarityCode { get; set; } = string.Empty;
        public string Card_Type { get; set; } = string.Empty;
        public string Card_Text { get; set; } = string.Empty;
        public string PageNumber { get; set; } = string.Empty;
        public string PageSize { get; set; } = string.Empty;
        public string Sort { get; set; } = "asc";
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
            httpClient.DefaultRequestHeaders.Add("api-version", "1.5");

            var queryParams = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(Name))
                queryParams.Add("Name", Name);
            if (!string.IsNullOrEmpty(SetId))
                queryParams.Add("SetId", SetId);
            if (!string.IsNullOrEmpty(ArtistName))
                queryParams.Add("ArtistName", ArtistName);
            if (!string.IsNullOrEmpty(RarityCode))
                queryParams.Add("RarityCode", RarityCode);
            if (!string.IsNullOrEmpty(Card_Type))
                queryParams.Add("Type", Card_Type);
            if (!string.IsNullOrEmpty(Card_Text))
                queryParams.Add("Text", Card_Text);
            if (!string.IsNullOrEmpty(PageNumber))
                queryParams.Add("PageNumber", PageNumber);
            if (!string.IsNullOrEmpty(PageSize))
                queryParams.Add("PageSize", PageSize);

            queryParams.Add("sort", Sort);

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
}
