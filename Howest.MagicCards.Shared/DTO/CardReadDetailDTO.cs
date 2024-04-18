using Howest.MagicCards.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.Shared.DTO;

public class CardReadDetailDTO
{
    public string CardName { get; set; }

    public string ConvertedManaCost { get; set; }

    public string Type { get; set; }

    public string Rarity { get; set; }

    public string Set { get; set; }

    public string Text { get; set; }

    public string ArtistName { get; set; }

    public string Flavor { get; set; }

    public string Number { get; set; }

    public string Power { get; set; }

    public string Toughness { get; set; }

    public string OriginalImageUrl { get; set; }

    public string OriginalType { get; set; }

    public virtual ICollection<CardColor> CardColors { get; set; } = new List<CardColor>();

    public virtual ICollection<CardType> CardTypes { get; set; } = new List<CardType>();
}
