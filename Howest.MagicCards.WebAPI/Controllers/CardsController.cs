using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.DAL.Repositories;
using Howest.MagicCards.Shared.Filters;
using Howest.MagicCards.WebAPI.NewFolder;
using Howest.MagicCards.WebAPI.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Howest.MagicCards.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CardsController : ControllerBase
{
    private readonly ICardRepository _cardRepo;

    public CardsController(ICardRepository cardRepo)
    {
        _cardRepo = cardRepo;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResponse<IEnumerable<Card>>), 200)]
    [ProducesResponseType(typeof(string), 404)]
    [ProducesResponseType(typeof(string), 500)]
    public ActionResult<PagedResponse<IEnumerable<Card>>> GetCards(
                                                                [FromQuery] CardFilter filter,
                                                                // IMapper mapper,
                                                                IOptionsSnapshot<ApiBehaviourConf> options)
    {
        try
        {
            filter.PageSize = options.Value.MaxPageSize;

            Console.WriteLine(filter.PageSize.ToString());
            if (filter.PageSize <= 0) { }
            return Ok(new PagedResponse<IEnumerable<Card>>(
                _cardRepo.getAllCards()
                            .Skip((filter.PageNumber - 1) * filter.PageSize)
                            .Take(filter.PageSize),
                filter.PageNumber,
                filter.PageSize
                ));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"could not fetch cards because: {ex.Message}");
        }


    }
}
