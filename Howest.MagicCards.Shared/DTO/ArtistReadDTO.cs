using Howest.MagicCards.DAL.Models;

namespace Howest.MagicCards.Shared.DTO
{
    public class ArtistReadDTO
    {
        public string FullName { get; set; }

        public virtual ICollection<Card> Cards { get; set; } = new List<Card>();
    }
}
