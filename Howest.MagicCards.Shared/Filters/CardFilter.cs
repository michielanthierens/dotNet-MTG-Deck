namespace Howest.MagicCards.Shared.Filters;
public class CardFilter : PaginationFilter
{
    public string? Name { get; set; } = default;
    public string? SetId { get; set; } = default;
    public string? ArtistName { get; set; } = default;
    public string? RarityCode { get; set; } = default;
    public string? Type { get; set; } = default;
    public string? Text { get; set; } = default;
}
