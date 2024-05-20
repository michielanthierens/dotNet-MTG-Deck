using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.Shared.DTO;
using Howest.MagicCards.WebAPI.Wrappers;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Json;

namespace Howest.MagicCards.Web.Components.Pages
{
    public partial class FilteredCardsList
    {
        //public string message { get; set; }

        //public async Task AddCardToDeck(CardReadDTO card)
        //{
        //    HttpClient httpClient = httpClientFactory.CreateClient("MinimalAPI");
        //    var content = new StringContent(string.Empty);

        //    HttpResponseMessage response = await httpClient.PutAsync($"add?id={card.Number}_{card.Name}&name={card.Name}", content);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        // todo refresh deck
        //    }
        //    else
        //    {
        //        message = $"Error: {response.ReasonPhrase}";
        //    }
        //}


        //public async Task addtest()
        //{
        //    HttpClient httpClient = httpClientFactory.CreateClient("MinimalAPI");
        //    var content = new StringContent(string.Empty);
        //    HttpResponseMessage response = await httpClient.PutAsync($"add?id=1_test&name=test", content);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        // todo refresh deck
        //    }
        //    else
        //    {
        //        message = $"Error: {response.ReasonPhrase}";
        //    }
        //}
    }
}
