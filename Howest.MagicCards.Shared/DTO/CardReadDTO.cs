using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.Shared.DTO
{
    public class CardReadDTO
    {
        public string Number { get; set; }

        public string CardName { get; set; }

        // todo remove these after testing
        public string Type { get; set; }

        public string Rarity { get; set; }

        public string Set { get; set; }

        public string Text { get; set; }

        public string ArtistName { get; set; }

        // use image for blazor application
        // public string OriginalImageUrl { get; set; }

    }
}
