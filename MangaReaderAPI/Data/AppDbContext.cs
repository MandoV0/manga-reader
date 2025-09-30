using Microsoft.EntityFrameworkCore;
using MangaReaderAPI.Models;

namespace MangaReaderAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Series> Series { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Series>()
                .Property(s => s.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Series>()
                .HasMany(s => s.Genres)
                .WithMany(g => g.Series)
                .UsingEntity(j => j.ToTable("SeriesGenres"));
        }
    }
}