using AutoMapper;
using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.Shared.DTO;


namespace Howest.MagicCards.Shared.Mappings;

public class CardsProfile : Profile
{
    public CardsProfile()
    {
        CreateMap<Card, CardReadDetailDTO>()
            .ForMember(dto => dto.CardName, opt => opt.MapFrom(c => c.Name))
            .ForMember(dto => dto.Rarity,opt => opt.MapFrom(r => r.RarityCodeNavigation.Name))
            .ForMember(dto => dto.Set, opt => opt.MapFrom(s => s.SetCodeNavigation.Name))
            .ForMember(dto => dto.ArtistName, opt => opt.MapFrom(a => a.Artist.FullName));

        CreateMap<Card, CardReadDTO>()
            .ForMember(dto => dto.MtgId, opt => opt.MapFrom(c => c.MtgId))
            .ForMember(dto => dto.OriginalImageUrl, opt => opt.MapFrom(c => c.OriginalImageUrl));

        CreateMap<Rarity, RarityDTO>()
            .ForMember(dto => dto.RarityCode, opt => opt.MapFrom(r => r.Code))
            .ForMember(dto => dto.Rarity, opt => opt.MapFrom(r => r.Name));

        CreateMap<DeckCard, DeckReadDTO>()
            .ForMember(dto => dto.MtgId, opt => opt.MapFrom(dc => dc.Id));

        CreateMap<DeckCard, DeckPutDTO>()
            .ForMember(dto => dto.Id, opt => opt.MapFrom((dc => dc.Id)))
            .ForMember(dto => dto.Name, opt => opt.MapFrom((dc => dc.Name)));
        
    }

    
}