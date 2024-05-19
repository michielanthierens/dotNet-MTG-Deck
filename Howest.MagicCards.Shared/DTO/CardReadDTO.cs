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

        public string Name { get; set; }

        // use image for blazor application
        public string OriginalImageUrl { get; set; }

    }
}
