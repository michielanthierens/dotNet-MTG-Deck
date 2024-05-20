using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.Shared.DTO;
using Howest.MagicCards.WebAPI.Wrappers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Json;

namespace Howest.MagicCards.Web.Components.Pages
{
    public partial class FilteredCardsList
    {
        [Parameter]
        public IEnumerable<CardReadDTO> Cards { get; set; }

        // below is in comments in partial class
        public string message { get; set; }

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
