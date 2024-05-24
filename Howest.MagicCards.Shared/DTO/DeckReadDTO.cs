namespace Howest.MagicCards.Shared.DTO
{
    public record DeckReadDTO
    {
        public string MtgId { get; set; }

        public string Name { get; set; }

        public int Amount { get; set; }
    }
}
