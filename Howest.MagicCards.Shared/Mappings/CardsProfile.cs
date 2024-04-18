using AutoMapper;
using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Howest.MagicCards.Shared.Mappings;

public class CardsProfile: Profile
{
    public CardsProfile()
    {
        CreateMap<Card, CardReadDetailDTO>()
            .ForMember(dto => dto.CardName, opt => opt.MapFrom(p => p.Name))
            .ForMember(dto => dto.Rarity, opt => opt.MapFrom(p => p.RarityCodeNavigation.Name))
            .ForMember(dto => dto.Set, opt => opt.MapFrom(p => p.SetCodeNavigation.Name))
            .ForMember(dto => dto.ArtistName, opt => opt.MapFrom(p => p.Artist.FullName));
    }
}