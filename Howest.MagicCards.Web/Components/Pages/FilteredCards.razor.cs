using Howest.MagicCards.DAL.Models;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace Howest.MagicCards.Web.Components.Pages
{
    public class FilteredCards
    {
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
                                                                    $"Text={CardText}&" +
                                                                    $"MaxPageSize={MaxPageSize}&" +
                                                                    $"PageNumber={PageNumber}&" +
                                                                    $"PageSize={PageNumber}&" +
                                                                    $"sort=asc");


        }

    }
}
