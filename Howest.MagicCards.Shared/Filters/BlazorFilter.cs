using System.ComponentModel.DataAnnotations;

namespace Howest.MagicCards.Shared.Filters
{
    public class BlazorFilter
    {  
        public string? Name { get; set; }
        public string? Set { get; set; }
        public string? ArtistName { get; set; }
        public string? RarityCode { get; set; }
        public string? Type { get; set; }
        public string? Text { get; set; }
        [Required]
        public string? Sort { get; set; } = "asc";
        [Required]
        [Range(0, 150)]
        public int? PageSize { get; set; } = 150;
        [Required]
        [Range(0, 70000)]
        public int? PageNumber { get; set; } = 1;
    }
}
