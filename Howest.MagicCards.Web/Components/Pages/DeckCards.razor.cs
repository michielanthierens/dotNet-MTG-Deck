﻿using Howest.MagicCards.Shared.DTO;
using Microsoft.AspNetCore.Components;
using System.Text;
using System.Text.Json;
using Howest.MagicCards.Shared.Wrappers;

namespace Howest.MagicCards.Web.Components.Pages;

public partial class DeckCards
{
    [Parameter]
    public IEnumerable<DeckReadDTO> deckCards { get; set; }
    [Parameter]
    public string message { get; set; }
    [Parameter]
    public int? AmountOfCardsInDeck { get; set; }
    private JsonSerializerOptions JsonOptions { get; }

    public DeckCards()
    {
        JsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
    }

    protected override async Task OnInitializedAsync()
    {
        await GetDeck();
    }

    protected override async Task OnParametersSetAsync()
    {
        StateHasChanged();
    }

    public async Task GetDeck()
    {
        HttpClient httpClient = httpClientFactory.CreateClient("MinimalAPI");

        HttpResponseMessage response = await httpClient.GetAsync("");

        string apiResponse = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            Response<IEnumerable<DeckReadDTO>> apiResponseObj = JsonSerializer.Deserialize<Response<IEnumerable<DeckReadDTO>>>(apiResponse, JsonOptions);
            if (apiResponseObj != null && apiResponseObj.Succeeded)
            {
                deckCards = apiResponseObj.Data ?? new List<DeckReadDTO>();
                message = null;
                CalculateAmount(deckCards);
            }
            else
            {
                message = apiResponseObj?.Message;
            }
        }
        else
        {
            message = $"Error: {response.ReasonPhrase}";
        }
    }

    private void CalculateAmount(IEnumerable<DeckReadDTO> cards)
    {
        AmountOfCardsInDeck = 0;
        foreach (DeckReadDTO card in cards)
        {
            AmountOfCardsInDeck += card.Amount;
        }
    }

    async Task RemoveCardFromDeck(DeckReadDTO card)
    {
        HttpClient httpClient = httpClientFactory.CreateClient("MinimalAPI");
        StringContent content = new StringContent(string.Empty);
            
        DeckPutDTO updateCard = new DeckPutDTO { Id = card.MtgId, Name = card.Name };

        string json = JsonSerializer.Serialize(updateCard);

        content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await httpClient.PutAsync($"remove", content);

        if (response.IsSuccessStatusCode)
        {
            await GetDeck();
        }
        else
        {
            message = $"Error: could not remove card";
        }
    }

    public async Task AddCardToDeck(DeckReadDTO card)
    {
        HttpClient httpClient = httpClientFactory.CreateClient("MinimalAPI");
        StringContent content = new StringContent(string.Empty);

        DeckPutDTO updateCard = new DeckPutDTO { Id = card.MtgId, Name = card.Name };

        string json = JsonSerializer.Serialize(updateCard);

        content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await httpClient.PutAsync($"add", content);

        if (response.IsSuccessStatusCode)
        {
            await GetDeck();
        }
        else
        {
            message = $"Error: could not add card";
        }
    }

    private async Task ClearDeck()
    {
        HttpClient httpClient = httpClientFactory.CreateClient("MinimalAPI");
        StringContent content = new StringContent(string.Empty);

        HttpResponseMessage response = await httpClient.DeleteAsync($"clear");

        if (response.IsSuccessStatusCode)
        {
            await GetDeck();
        }
        else
        {
            message = $"Error: could not clear deck";
        }
    }
}