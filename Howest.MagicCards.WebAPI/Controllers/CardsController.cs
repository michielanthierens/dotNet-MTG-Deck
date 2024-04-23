using AutoMapper;
using AutoMapper.QueryableExtensions;
using Howest.MagicCards.DAL.Repositories;
using Howest.MagicCards.Shared.DTO;
using Howest.MagicCards.Shared.Extensions;
using Howest.MagicCards.Shared.Filters;
using Howest.MagicCards.WebAPI.BehaviourConf;
using Howest.MagicCards.WebAPI.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Howest.MagicCards.WebAPI.Controllers;

[ApiVersion("1.1")]
[ApiVersion("1.5")]
[Route("api/[controller]")]
[ApiController]
public class CardsController : ControllerBase
{
    private readonly ICardRepository _cardRepo;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;

    public CardsController(ICardRepository cardRepo, IMapper mapper, IMemoryCache memoryCache)
    {
        _cardRepo = cardRepo;
        _mapper = mapper;
        _cache = memoryCache;
    }


    // filter on:
    // set
    // artist (id)
    // rarity
    // card type
    // card text

    // sort on:
    // card name (desc/asc) (1.5)

    // show details of a card (1.5)

    [HttpGet]
    [ProducesResponseType(typeof(PagedResponse<IEnumerable<CardReadDTO>>), 200)]
    [ProducesResponseType(typeof(Response<CardReadDTO>), 500)]
    public async Task<ActionResult<PagedResponse<IEnumerable<CardReadDTO>>>> GetCards(
                                                                [FromQuery] CardFilter filter,
                                                                IOptionsSnapshot<ApiBehaviourConf> options)
    {
        filter.MaxPageSize = options.Value.MaxPageSize;

        string _key = $"CardsKey-{filter.MaxPageSize}_{filter.PageSize}_{filter.PageNumber}_{filter.Name}_{filter.Set}_{filter.Artist}_{filter.Rarity}_{filter.Type}_{filter.Text}";

        try
        {
            if (!_cache.TryGetValue(_key, out IEnumerable<CardReadDTO> cachedResult))
            {
                cachedResult = await _cardRepo.getAllCards()
                            .ToFilteredList(filter.Name, filter.Set, filter.Artist, filter.Rarity, filter.Type, filter.Text)
                            .ToPagedList(filter.PageNumber, filter.PageSize)
                            .ProjectTo<CardReadDTO>(_mapper.ConfigurationProvider)
                            .ToListAsync();

                MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                };

                _cache.Set(_key, cachedResult, cacheOptions);
            }

            return Ok(new PagedResponse<IEnumerable<CardReadDTO>>(
                cachedResult,
                filter.PageNumber,
                filter.PageSize
                )
            {
                TotalRecords = _cardRepo
                                    .getAllCards()
                                    .ToFilteredList(filter.Name, filter.Set, filter.Artist, filter.Rarity, filter.Type, filter.Text)
                                    .Count()
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new Response<CardReadDTO>()
                {
                    Succeeded = false,
                    Errors = [$"Status code: {StatusCodes.Status500InternalServerError}"],
                    Message = $"({ex.Message})"
                });
        }
    }
}
