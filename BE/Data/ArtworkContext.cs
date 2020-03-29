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
            
            
        }

        public DbSet<Artwork> Artworks { get; set; }
    }
}
