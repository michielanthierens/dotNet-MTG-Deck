using AutoMapper;
using AutoMapper.QueryableExtensions;
using Howest.MagicCards.DAL.Repositories;
using Howest.MagicCards.Shared.DTO;
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
    // todo push key into settings    
    private readonly ICardRepository _cardRepo;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;

    public CardsController(ICardRepository cardRepo, IMapper mapper, IMemoryCache memoryCache)
    {
        _cardRepo = cardRepo;
        _mapper = mapper;
        _cache = memoryCache;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResponse<IEnumerable<CardReadDetailDTO>>), 200)]
    [ProducesResponseType(typeof(Response<CardReadDetailDTO>), 500)]
    public async Task<ActionResult<PagedResponse<IEnumerable<CardReadDetailDTO>>>> GetCards(
                                                                [FromQuery] CardFilter filter, IOptionsSnapshot<ApiBehaviourConf> options)
    {
        string _key = $"CardsKey{filter.MaxPageSize}{filter.PageSize}{filter.PageNumber}";
        filter.MaxPageSize = options.Value.MaxPageSize;
        try
        {
            if (!_cache.TryGetValue(_key, out IEnumerable<CardReadDetailDTO> cachedResult))
            {
                cachedResult = await _cardRepo.getAllCards()
                            .Skip((filter.PageNumber - 1) * filter.PageSize)
                            .Take(filter.PageSize)
                            .ProjectTo<CardReadDetailDTO>(_mapper.ConfigurationProvider)
                            .ToListAsync();

                MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                };

                _cache.Set(_key, cachedResult, cacheOptions);
            }

            return Ok(new PagedResponse<IEnumerable<CardReadDetailDTO>>(
                cachedResult,
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
