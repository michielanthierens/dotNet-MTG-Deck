using AutoMapper;
using AutoMapper.QueryableExtensions;
using Howest.MagicCards.DAL.Models;
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

namespace Howest.MagicCards.WebAPI.Controllers.V1_1
{
    [ApiVersion("1.1")]
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

        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<CardReadDTO>>), 200)]
        [ProducesResponseType(typeof(Response<CardReadDTO>), 500)]
        public async Task<ActionResult<PagedResponse<IEnumerable<CardReadDTO>>>> GetCards(
                                                                    [FromQuery] CardFilter filter,
                                                                    IOptionsSnapshot<ApiBehaviourConf> options)
        {
            filter.MaxPageSize = options.Value.MaxPageSize;

            string _key = $"CardsKey-{filter.MaxPageSize}_{filter.PageSize}_{filter.PageNumber}_{filter.Name}_{filter.Set}_{filter.ArtistName}_{filter.RarityCode}_{filter.Type}_{filter.Text}";

            try
            {
                if (!_cache.TryGetValue(_key, out IEnumerable<CardReadDTO>? cachedResult))
                {
                    cachedResult = await _cardRepo.getAllCards()
                                .ToFilteredList(filter.Name, filter.Set, filter.ArtistName, filter.RarityCode, filter.Type, filter.Text)
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
                                        .ToFilteredList(filter.Name, filter.Set, filter.ArtistName, filter.RarityCode, filter.Type, filter.Text)
                                        .Count()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response<CardReadDTO>() //to inenumerable
                    {
                        Succeeded = false,
                        Errors = [$"Status code: {StatusCodes.Status500InternalServerError}"],
                        Message = $"({ex.Message})"
                    });
            }
        }
    }
}

namespace Howest.MagicCards.WebAPI.Controllers.V1_5
{
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

        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<CardReadDTO>>), 200)]
        [ProducesResponseType(typeof(Response<CardReadDTO>), 500)]
        public async Task<ActionResult<PagedResponse<IEnumerable<CardReadDTO>>>> GetCards(
                                                                    [FromQuery] CardFilter filter,
                                                                    [FromQuery] string sort,
                                                                    IOptionsSnapshot<ApiBehaviourConf> options)
        {
            filter.MaxPageSize = options.Value.MaxPageSize;

            string _key = $"CardsKey-{filter.MaxPageSize}_{filter.PageSize}_{filter.PageNumber}_{filter.Name}_{filter.Set}_{filter.ArtistName}_{filter.RarityCode}_{filter.Type}_{filter.Text}_{sort}";

            try
            {
                if (!_cache.TryGetValue(_key, out IEnumerable<CardReadDTO>? cachedResult))
                {
                    cachedResult = await _cardRepo.getAllCards()
                                .ToFilteredList(filter.Name, filter.Set, filter.ArtistName, filter.RarityCode, filter.Type, filter.Text)
                                .SortOnCardName(sort)
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
                                        .ToFilteredList(filter.Name, filter.Set, filter.ArtistName, filter.RarityCode, filter.Type, filter.Text)
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

        [HttpGet("{id}", Name = "getCardDetail")]
        [ProducesResponseType(typeof(Response<CardReadDetailDTO>), 200)]
        [ProducesResponseType(typeof(Response<CardReadDetailDTO>), 404)]
        [ProducesResponseType(typeof(Response<CardReadDetailDTO>), 500)]
        public async Task<ActionResult<Response<CardReadDetailDTO>>> GetCardDetail(string id)
        {
            string _key = $"CardDetail-{id}";

            try
            {
                if (_cache.TryGetValue(_key, out CardReadDetailDTO? cachedCard)) return Ok(new Response<CardReadDetailDTO>(cachedCard));

                Card card = await _cardRepo.GetCardbyId(id);

                if (card is not Card)
                {
                    return NotFound($"No book found with id {id}");
                }
                
                CardReadDetailDTO foundCard = _mapper.Map<CardReadDetailDTO>(card);

                MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                };

                _cache.Set(_key, foundCard, cacheOptions);

                return Ok(value: new Response<CardReadDetailDTO>(foundCard));

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

        [HttpGet("rarities", Name = "getRarities")]
        [ProducesResponseType(typeof(Response<CardReadDetailDTO>), 200)]
        [ProducesResponseType(typeof(Response<CardReadDetailDTO>), 500)]
        public async Task<ActionResult<Response<IEnumerable<RarityDTO>>>> GetRariries()
        {
            string _key = $"All-Rarities";

            try
            {
                if (!_cache.TryGetValue(_key, out IEnumerable<RarityDTO>? cachedResult))
                {
                    cachedResult = await _cardRepo.GetRarities()                                
                                .ProjectTo<RarityDTO>(_mapper.ConfigurationProvider)
                                .ToListAsync();

                    MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                    };

                    _cache.Set(_key, cachedResult, cacheOptions);
                }

                return Ok(cachedResult);

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
}
