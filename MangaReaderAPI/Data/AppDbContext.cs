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
        public DbSet<User> Users { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<SeriesView> SeriesViews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            /* Stores the ENUM SeriesStatus Value as a string */
            modelBuilder.Entity<Series>()
                .Property(s => s.Status)
                .HasConversion<string>();

            /* Set up Many-to-Many relationship between Series and Genre.
             * This creates a join table named "SeriesGenres" with SeriesId and GenreId as foreign keys.
             */
            modelBuilder.Entity<Series>()
                .HasMany(s => s.Genres)
                .WithMany(g => g.Series)
                .UsingEntity(j => j.ToTable("SeriesGenres"));

            /* Ensures one user can only have one view per Series.
             * 
             */
            modelBuilder.Entity<SeriesView>()
                .HasIndex(v => new { v.SeriesId, v.UserId })
                .IsUnique();

            modelBuilder.Entity<Rating>()
                .HasIndex(r => new { r.SeriesId, r.UserId })
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}