using BE.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Data
{
    public class ArtworkContext : DbContext
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

            builder.Entity<Artwork>().HasData(
                 new Artwork { Id = 1, Name = "Luffy", Created = DateTime.Now, Artist = "Bright" },
                 new Artwork { Id = 2, Name = "Chen", Created = DateTime.Now });

            builder.Entity<Tag>().HasData(
                    new { Id = 1, Name = "Anime", ArtworkId = 1 },
                    new { Id = 2, Name = "ONe piece", ArtworkId = 1 },
                    new { Id = 3, Name = "arknights", ArtworkId = 2 }
                 );
        }

        public DbSet<Artwork> Artworks { get; set; }
    }
}
