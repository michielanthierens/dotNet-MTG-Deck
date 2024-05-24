using AutoMapper;
using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.DAL.Repositories;
using Howest.MagicCards.Shared.DTO;
using Howest.MagicCards.WebAPI.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace Howest.MagicCards.MinimalAPI.extensions;

public static class DeckEndpoints
{
    public static void MapDeckEndpoints(this RouteGroupBuilder deckGroup)
    {

        deckGroup.MapGet("", async (IDeckRepository deckRepo, IMapper mapper) =>
        {
            try
            {
                IEnumerable<DeckCard> fetchedDeck = await deckRepo.GetDeck();
                IEnumerable<DeckReadDTO> deck = mapper.Map<IEnumerable<DeckReadDTO>>(fetchedDeck);
                return Results.Ok(new Response<IEnumerable<DeckReadDTO>>()
                {
                    Succeeded = true,
                    Data = deck,
                    Message = $"deck fetched"
                });
            }
            catch (Exception ex)
            {
                return Results.StatusCode(StatusCodes.Status500InternalServerError);         
            }
        });

        deckGroup.MapPut("/add", async (DeckPutDTO newCard, [FromServices] IDeckRepository deckRepo) =>
        {

            await deckRepo.addCardToDeckAsync(newCard.Id, newCard.Name);
            return Results.Ok(new Response<DeckPutDTO>
            {
                Succeeded = true,
                Data = newCard,
                Message = $"card updated"
            });
        });

        deckGroup.MapPut("/remove", async (DeckPutDTO newCard, [FromServices] IDeckRepository deckRepo) =>
        {
            deckRepo.removeCardFromDeck(newCard.Id);
            return Results.Ok(new Response<DeckPutDTO>
            {
                Succeeded = true,
                Data = newCard,
                Message = $"card updated"
            });
        });

        deckGroup.MapDelete("/clear", async ([FromServices] IDeckRepository deckrepo) =>
        {
            await deckrepo.clearDeck();
            return Results.Ok(new Response<DeckReadDTO>
            {
                Succeeded = true,
                Data = null,
                Message = $"deck cleared"
            });
        });
    }
}
