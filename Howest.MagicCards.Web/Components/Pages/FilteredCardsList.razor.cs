using Howest.MagicCards.Shared.DTO;
using Microsoft.AspNetCore.Components;


namespace Howest.MagicCards.Web.Components.Pages
{
    public partial class FilteredCardsList
    {
        [Parameter]
        public IEnumerable<CardReadDTO> Cards { get; set; }

        public string message { get; set; }

        public async Task AddCardToDeck(CardReadDTO card)
        {
            HttpClient httpClient = httpClientFactory.CreateClient("MinimalAPI");
            var content = new StringContent(string.Empty);

            HttpResponseMessage response = await httpClient.PutAsync($"add?id={card.Number}_{card.Name}&name={card.Name}", content);

            if (response.IsSuccessStatusCode)
            {
                // reset deck.
            }
            else
            {
                message = $"Error: {response.ReasonPhrase}";
            }
        }
    }
}
