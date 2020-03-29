using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Models
{
    public interface IArtworkRepository
    {
        Artwork GetBy(int id);
        bool TryGetArtwork(int id, out Artwork artwork);
        IEnumerable<Artwork> GetAll();
        IEnumerable<Artwork> GetBy(string name = null, string artist = null, string tagName = null);
        void Add(Artwork artwork);
        void Delete(Artwork artwork);
        void Update(Artwork artwork);
        void SaveChanges();
    }
}
