using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.Shared.Filters
{
    public class BlazorFilter
    {
        public string? Name { get; set; }
        public string? SetId { get; set; }
        public string? ArtistName { get; set; }
        public string? RarityCode { get; set; }
        public string? Type { get; set; }
        public string? Text { get; set; }
        public string? Sort { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber {  get; set; }
    }
}
