using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Models
{
    public class CustomerFavorite
    {
        #region Properties
        public int CustomerId { get; set; }

        public int ArtworkId { get; set; }

        public Customer Customer { get; set; }

        public Artwork Artwork { get; set; }
        #endregion
    }
}
