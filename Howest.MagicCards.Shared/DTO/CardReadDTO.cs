namespace Howest.MagicCards.Shared.DTO
{
    public record CardReadDTO
    {
        public string MtgId { get; set; }

        public string Name { get; set; }

        public string OriginalImageUrl { get; set; }

    }
}
