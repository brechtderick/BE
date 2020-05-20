using BE.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Data
{
    public class ArtworkContext : IdentityDbContext
    {
        public ArtworkContext(DbContextOptions<ArtworkContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Artwork>()
                .HasMany(a => a.Tags)
                .WithOne()
                .IsRequired()
                .HasForeignKey("ArtworkId");
            builder.Entity<Artwork>().Property(a => a.Name).IsRequired().HasMaxLength(100);
            builder.Entity<Artwork>().Property(a => a.Artist).HasMaxLength(50);
            builder.Entity<Tag>().Property(a => a.Name).IsRequired().HasMaxLength(100);

            builder.Entity<Customer>().Property(c => c.LastName).IsRequired().HasMaxLength(50);
            builder.Entity<Customer>().Property(c => c.FirstName).IsRequired().HasMaxLength(50);
            builder.Entity<Customer>().Property(c => c.Email).IsRequired().HasMaxLength(100);
            builder.Entity<Customer>().Ignore(c => c.FavoriteArtworks);

            builder.Entity<CustomerFavorite>().HasKey(f => new { f.CustomerId, f.ArtworkId });
            builder.Entity<CustomerFavorite>().HasOne(f => f.Customer).WithMany(u => u.Favorites).HasForeignKey(f => f.CustomerId);
            builder.Entity<CustomerFavorite>().HasOne(f => f.Artwork).WithMany().HasForeignKey(f => f.ArtworkId);

            builder.Entity<Artwork>().HasData(
                 new Artwork { Id = 1, Name = "Luffy", Created = DateTime.Now, Artist = "Bright" },
                 new Artwork { Id = 2, Name = "Chen", Created = DateTime.Now });

            builder.Entity<Tag>().HasData(
                    new { Id = 1, Name = "Anime", ArtworkId = 1 },
                    new { Id = 2, Name = "One piece", ArtworkId = 1 },
                    new { Id = 3, Name = "Arknights", ArtworkId = 2 }
                 );
        }

        public DbSet<Artwork> Artworks { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
