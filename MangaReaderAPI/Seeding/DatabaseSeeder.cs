using Bogus;
using MangaReaderAPI.Data;
using MangaReaderAPI.Models;
using MangaReaderAPI.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace MangaReaderAPI.Seeding
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(AppDbContext context, int seriesCount = 50)
        {
            if (await context.Series.AnyAsync()) return;

            var genres = new List<Genre>
            {
                new Genre { Name = "Action"},
                new Genre { Name = "Adventure "},
                new Genre { Name = "Romance "},
                new Genre { Name = "Comedy" },
                new Genre { Name = "Drama"},
                new Genre { Name = "Fantasy" },
                new Genre { Name = "Horror"},
                new Genre { Name = "Mystery"},
                new Genre { Name = "Sci-Fi"},
            };

            foreach (var g in genres)
            {
                if (!await context.Genres.AnyAsync(x => x.Name == g.Name))
                {
                    await context.Genres.AddAsync(g);
                }
            }
            await context.SaveChangesAsync();

            var seriesFaker = new Faker<Series>()
                .RuleFor(s => s.Title, f => f.Lorem.Sentence(3, 20))
                .RuleFor(s => s.Description, f => f.Lorem.Text())
                .RuleFor(s => s.Author, f => f.Person.FullName)
                .RuleFor(s => s.Publisher, f => f.Company.CompanyName())
                .RuleFor(s => s.Status, f => f.PickRandom<SeriesStatus>())
                .RuleFor(s => s.CoverImageUrl, f => "https://placehold.co/300x400")
                .RuleFor(s => s.ReleaseDate, f => f.Date.Past(20).ToUniversalTime())
                .RuleFor(s => s.AverageRating, f => Math.Round(f.Random.Double(1.0, 5.0), 1))
                .RuleFor(s => s.Genres, f => f.PickRandom(genres, f.Random.Int(1, 3)).ToList())
                .RuleFor(s => s.Chapters, f =>
                {
                    var chapterCount = f.Random.Int(5, 50);
                    var chapters = new List<Chapter>();

                    for (int i = 1; i <= chapterCount; i++)
                    {
                        int pageCount = f.Random.Int(10, 30);
                        var pages = new List<Page>();

                        for (int j = 0; j < pageCount; j++)
                        {
                            pages.Add(new Page
                            {
                                ImageUrl = "https://placehold.co/300x400",
                                Index = j
                            });
                        }

                        chapters.Add(new Chapter
                        {
                            Title = $"Chapter {i}",
                            Pages = pages
                        });
                    }

                    return chapters;
                });


            var seriesList = seriesFaker.Generate(seriesCount);

            await context.Series.AddRangeAsync(seriesList);
            await context.SaveChangesAsync();

            Console.WriteLine($"Seeded {seriesCount} series with chapters and genres.");
        }
    }
}