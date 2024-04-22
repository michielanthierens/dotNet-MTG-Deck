using Howest.MagicCards.DAL.Models;

namespace Howest.MagicCards.DAL.Repositories
{
    public interface IArtistRepository
    {
        IQueryable<Artist> GetAllArtists();
        Artist GetArtistById(int id);
    }
}