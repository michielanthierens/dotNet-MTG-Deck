﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.Shared.DTO
{
    public record RarityDTO
    {
        public string RarityCode { get; set; }
        public string Rarity { get; set; }
    }
}
