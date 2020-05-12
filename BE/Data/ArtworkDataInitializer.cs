using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Data
{
    public class ArtworkDataInitializer
    {
        private readonly ArtworkContext _dbContext;

        public ArtworkDataInitializer(ArtworkContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                            
            }
        }
    }
}
