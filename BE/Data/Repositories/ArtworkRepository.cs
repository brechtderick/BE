using BE.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Data.Repositories
{
    public class ArtworkRepository : IArtworkRepository
    {
        private readonly ArtworkContext _context;
        private readonly DbSet<Artwork> _artworks;

        public ArtworkRepository(ArtworkContext dbContext)
        {
            _context = dbContext;
            _artworks = dbContext.Artworks;
        }

        public IEnumerable<Artwork> GetAll()
        {
            return _artworks.Include(a => a.Tags).ToList();
        }

        public Artwork GetBy(int id)
        {
            return _artworks.Include(a => a.Tags).SingleOrDefault(a => a.Id == id);
        }

        public bool TryGetArtwork(int id, out Artwork artwork)
        {
            artwork = _context.Artworks.Include(a => a.Tags).FirstOrDefault(a => a.Id == id);
            return artwork != null;
        }

        public void Add(Artwork artwork)
        {
            _artworks.Add(artwork);
        }

        public void Update(Artwork artwork)
        {
            _context.Update(artwork);
        }

        public void Delete(Artwork artwork)
        {
            _artworks.Remove(artwork);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IEnumerable<Artwork> GetBy(string name = null, string artist = null, string tagName = null)
        {
            var artworks = _artworks.Include(a => a.Tags).AsQueryable();
            if (!string.IsNullOrEmpty(name))
                artworks = artworks.Where(a => a.Name.IndexOf(name) >= 0);
            if (!string.IsNullOrEmpty(artist))
                artworks = artworks.Where(a => a.Artist == artist);
            if (!string.IsNullOrEmpty(tagName))
                artworks = artworks.Where(a => a.Tags.Any(t => t.Name == tagName));
            return artworks.OrderBy(a => a.Name).ToList();
        }
    }
}
