using Howest.MagicCards.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Howest.MagicCards.MinimalAPI.extensions;

public static class DeckEndpoints
{
    public static void MapDeckEndpoints(this RouteGroupBuilder deckGroup)
    {
        deckGroup.MapGet("", async (IDeckRepository deckRepo) =>
        {
            var deck = await deckRepo.GetDeck();
            return Results.Ok(deck);
        });

        deckGroup.MapPut("/add", async (string id, string name, [FromServices] IDeckRepository deckRepo) =>
        {

            deckRepo.addCardToDeckAsync(id, name);
            return Results.Ok("card updated");
        });

        deckGroup.MapPut("/remove", async (string id, [FromServices] IDeckRepository deckRepo) =>
        {
            deckRepo.removeCardFromDeck(id);
            return Results.Ok("card updated");
        });

        deckGroup.MapDelete("/clear", async ([FromServices] IDeckRepository deckrepo) =>
        {
            await deckrepo.clearDeck();
            return Results.Ok("deck cleared");

        });
    }
}
