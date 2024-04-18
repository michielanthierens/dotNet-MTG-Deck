using AutoMapper;
using AutoMapper.QueryableExtensions;
using Howest.MagicCards.DAL.Repositories;
using Howest.MagicCards.Shared.DTO;
using Howest.MagicCards.Shared.Filters;
using Howest.MagicCards.WebAPI.BehaviourConf;
using Howest.MagicCards.WebAPI.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Howest.MagicCards.WebAPI.Controllers;

[ApiVersion("1.1")]
[ApiVersion("1.5")]
[Route("api/[controller]")]
[ApiController]
[ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any)]
public class CardsController : ControllerBase
{
    private readonly ICardRepository _cardRepo;
    private readonly IMapper _mapper;

    public CardsController(ICardRepository cardRepo, IMapper mapper)
    {
        _cardRepo = cardRepo;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResponse<IEnumerable<CardReadDetailDTO>>), 200)]
    [ProducesResponseType(typeof(Response<CardReadDetailDTO>), 500)]
    public ActionResult<PagedResponse<IEnumerable<CardReadDetailDTO>>> GetCards(
                                                                [FromQuery] CardFilter filter, IOptionsSnapshot<ApiBehaviourConf> options)
    {
        filter.MaxPageSize = options.Value.MaxPageSize;
        try
        {
            return Ok(new PagedResponse<IEnumerable<CardReadDetailDTO>>(
                _cardRepo.getAllCards()
                            .Skip((filter.PageNumber - 1) * filter.PageSize)
                            .Take(filter.PageSize)
                            .ProjectTo<CardReadDetailDTO>(_mapper.ConfigurationProvider)
                            .ToList(),
                filter.PageNumber,
                filter.PageSize
                ));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new Response<CardReadDetailDTO>()
                {
                    Succeeded = false,
                    Errors = [$"Status code: {StatusCodes.Status500InternalServerError}"],
                    Message = $"({ex.Message})"
                });
        }
    }
}
