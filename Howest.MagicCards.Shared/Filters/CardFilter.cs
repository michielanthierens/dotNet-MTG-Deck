namespace Howest.MagicCards.Shared.Filters;
public class CardFilter : PaginationFilter
{
    public string set { get; set; } = default;
    public string artist { get; set; } = default;
    public string rarity { get; set; } = default;
    public string type { get; set; } = default;
    public string name { get; set; } = default;
    public string text { get; set; } = default;
}
