namespace Howest.MagicCards.DAL.Models;

public class Card
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? ManaCost { get; set; }
    public string ConvertedManaCost { get; }
    public string Type { get; set; }
    public string Rarity_code { get; set; }
    public string? Text { get; set; }
    public string? Flavor { get; set; }
    public int Artist_id { get; set; }
    public string Number { get; set; }
    public string? Power { get; set; }
    public string? Toughness { get; set; }
    public string Layout { get; set; }
    public int? multiverseId { get; set; }
    public string? Original_image_url { get; set; }
    public string Image { get; set; }
    public string? Original_text { get; set; }
    public string? Original_type { get; set; }
    public string Mtg_id { get; set; }
    public string? Variations { get; set; }
    public DateOnly? Created_at { get; set; }
    public DateOnly? Updated_at { get; set; }

}
