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
        public string? Sort { get; set; } = "asc";
        public int? PageSize { get; set; } = 150;
        public int? PageNumber { get; set; } = 1;
    }
}
