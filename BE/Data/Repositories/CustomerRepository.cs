using BE.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace BE.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ArtworkContext _context;
        private readonly DbSet<Customer> _customers;

        public CustomerRepository(ArtworkContext dbContext)
        {
            _context = dbContext;
            _customers = dbContext.Customers;
        }

        public Customer GetBy(string email)
        {
            return _customers.Include(c => c.Favorites).ThenInclude(f => f.Artwork).ThenInclude(a => a.Tags).SingleOrDefault(c => c.Email == email);
        }

        public void Add(Customer customer)
        {
            _customers.Add(customer);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
