namespace Howest.MagicCards.Shared.DTO;

public record CardReadDetailDTO
{
    public string CardName { get; set; }

    public string Power { get; set; }

    public string ConvertedManaCost { get; set; }

    public string Toughness { get; set; }

    public string Type { get; set; }

    public string Rarity { get; set; }

    public string Set { get; set; }

    public string Text { get; set; }

    public string ArtistName { get; set; }

    public string MtgId { get; set; }
}
