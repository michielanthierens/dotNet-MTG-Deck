using AutoMapper;
using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.Shared.DTO;


namespace Howest.MagicCards.Shared.Mappings;

public class CardsProfile : Profile
{
    public CardsProfile()
    {
        CreateMap<Card, CardReadDetailDTO>()
            .ForMember(dto => dto.CardName, opt => opt.MapFrom(p => p.Name))
            .ForMember(dto => dto.Rarity, opt => opt.MapFrom(p => p.RarityCodeNavigation.Name))
            .ForMember(dto => dto.Set, opt => opt.MapFrom(p => p.SetCodeNavigation.Name))
            .ForMember(dto => dto.ArtistName, opt => opt.MapFrom(p => p.Artist.FullName))
            .ForMember(dto => dto.CardColors, opt => opt.MapFrom(p => p.CardColors.Select(cc => cc.Color.Name)))
            .ForMember(dto => dto.CardTypes, opt => opt.MapFrom(p => p.CardTypes.Select(ct => ct.Type.Name)));

        CreateMap<Card, CardReadDTO>()
            .ForMember(dto => dto.Number, opt => opt.MapFrom(opt => opt.Number))
            .ForMember(dto => dto.OriginalImageUrl, opt => opt.MapFrom(opt => opt.OriginalImageUrl));
    }

    
}