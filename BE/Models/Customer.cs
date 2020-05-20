using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Models
{
    public class Customer
    {
        #region Properties
        //add extra properties if needed
        public int CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public ICollection<CustomerFavorite> Favorites { get; private set; }

        public IEnumerable<Artwork> FavoriteArtworks => Favorites.Select(f => f.Artwork);
        #endregion

        #region Constructors
        public Customer()
        {
            Favorites = new List<CustomerFavorite>();
        }
        #endregion

        #region Methods
        public void AddFavoriteArtwork(Artwork artwork)
        {
            Favorites.Add(new CustomerFavorite() { ArtworkId = artwork.Id, CustomerId = CustomerId, Artwork = artwork, Customer = this });
        }
        #endregion

    }
}
