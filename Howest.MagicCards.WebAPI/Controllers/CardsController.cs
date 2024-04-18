using AutoMapper;
using AutoMapper.QueryableExtensions;
using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.DAL.Repositories;
using Howest.MagicCards.Shared.Filters;
using Howest.MagicCards.WebAPI.Wrappers;
using Microsoft.AspNetCore.Mvc;

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
    public ActionResult<PagedResponse<IEnumerable<Card>>> GetCards([FromQuery] CardFilter filter, [FromServices] IConfiguration config)
    {
        filter.PageSize = int.Parse(config["MaxPageSize"]);

        return Ok(new PagedResponse<IEnumerable<Card>>(
            _cardRepo.getAllCards()
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize),
            filter.PageNumber,
            filter.PageSize
            ));
    }    
}
