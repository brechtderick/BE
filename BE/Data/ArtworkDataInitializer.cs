using BE.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Data
{
    public class ArtworkDataInitializer
    {
        private readonly ArtworkContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public ArtworkDataInitializer(ArtworkContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                //seeding the database with recipes, see DBContext         
                Customer customer = new Customer { Email = "recipemaster@hogent.be", FirstName = "Adam", LastName = "Master" };
                _dbContext.Customers.Add(customer);
                await CreateUser(customer.Email, "P@ssword1111");
                Customer student = new Customer { Email = "student@hogent.be", FirstName = "Student", LastName = "Hogent" };
                _dbContext.Customers.Add(student);
                student.AddFavoriteArtwork(_dbContext.Artworks.First());
                await CreateUser(student.Email, "P@ssword1111");
                _dbContext.SaveChanges();
            }
        }

        private async Task CreateUser(string email, string password)
        {
            var user = new IdentityUser { UserName = email, Email = email };
            await _userManager.CreateAsync(user, password);
        }
    }
}
