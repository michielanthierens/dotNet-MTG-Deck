﻿using Howest.MagicCards.DAL.DBContext;
using Howest.MagicCards.DAL.Models;

namespace Howest.MagicCards.DAL.Repositories
{
    public class SqlArtistRepository : IArtistRepository
    {
        private readonly MtgContext _db;

        public SqlArtistRepository(MtgContext db)
        {
            _db = db;
        }

        public IQueryable<Artist> GetAllArtists()
        {
            IQueryable<Artist> AllArtists = _db.Artists.Select(c => c);
            return AllArtists;
        }

        public Artist GetArtistById(long? id)
        {
            try
            {
                Artist artist = _db.Artists.FirstOrDefault(c => c.Id.Equals(id));
                return artist;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
