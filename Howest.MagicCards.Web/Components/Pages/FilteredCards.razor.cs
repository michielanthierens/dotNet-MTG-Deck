﻿using Howest.MagicCards.Shared.DTO;
using Howest.MagicCards.Shared.Filters;
using Howest.MagicCards.Shared.Wrappers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Json;

namespace Howest.MagicCards.Web.Components.Pages
{
    public partial class FilteredCards
    {
        BlazorFilter _blazorFilter = new();

        [Parameter]
        public EventCallback GetDeck { get; set; }

        private IEnumerable<CardReadDTO> filteredCards { get; set; }
        private string Message { get; set; }
        private JsonSerializerOptions JsonOptions { get; }

        public FilteredCards()
        {
            JsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadFilteredCards();
        }

        private async Task LoadFilteredCards()
        {
            HttpClient httpClient = httpClientFactory.CreateClient("WebAPI");
            httpClient.DefaultRequestHeaders.Add("api-version", "1.5");

            Dictionary<string, string> queryParams = new();

            if (!string.IsNullOrEmpty(_blazorFilter.Name))
                queryParams.Add("Name", _blazorFilter.Name);
            if (!string.IsNullOrEmpty(_blazorFilter.Set))
                queryParams.Add("Set", _blazorFilter.Set);
            if (!string.IsNullOrEmpty(_blazorFilter.ArtistName))
                queryParams.Add("ArtistName", _blazorFilter.ArtistName);
            if (!string.IsNullOrEmpty(_blazorFilter.RarityCode))
                queryParams.Add("RarityCode", _blazorFilter.RarityCode);
            if (!string.IsNullOrEmpty(_blazorFilter.Type))
                queryParams.Add("Type", _blazorFilter.Type);
            if (!string.IsNullOrEmpty(_blazorFilter.Text))
                queryParams.Add("Text", _blazorFilter.Text);
            if (_blazorFilter.PageNumber > 0)
                queryParams.Add("PageNumber", _blazorFilter.PageNumber.ToString());
            if (_blazorFilter.PageSize > 0)
                queryParams.Add("PageSize", _blazorFilter.PageSize.ToString());
            if (!string.IsNullOrEmpty(_blazorFilter.Sort))
                queryParams.Add("Sort", _blazorFilter.Sort);
            if (string.IsNullOrEmpty(_blazorFilter.Sort))
                queryParams.Add("Sort", "asc");

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
                Message = $"Error: {response.ReasonPhrase}";
            }
        }
    }
}
