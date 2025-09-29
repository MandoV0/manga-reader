using MangaReaderAPI.Models;

namespace MangaReaderAPI.Repositories
{
    public class SeriesRepository : ISeriesRepository
    {
        private readonly List<Series> _series;

        public SeriesRepository()
        {
            // Data for testing
            _series = new List<Series>
            {
                new Series { Id=1, Title="Example Manga 1", Author="Author A", Publisher="Publisher X", Status="Ongoing", ReleaseDate=DateTime.Now.AddMonths(-5), Genres=new List<string>{"Action","Adventure"}, AverageRating=4.2, Chapters=new List<Chapter>{ new Chapter{Id=1, Title="Chapter 1", PageCount=20, Pages=new List<string>{"page1.jpg","page2.jpg"}} } },
                new Series { Id=2, Title="Example Manga 2", Author="Author B", Publisher="Publisher Y", Status="Completed", ReleaseDate=DateTime.Now.AddMonths(-12), Genres=new List<string>{"Romance"}, AverageRating=4.7 }
            };
        }

        public Series? GetSeries(int id)
        {
            return _series.FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<Series> GetAllSeries()
        {
            return _series;
        }

        public IEnumerable<Series> GetTrending()
        {
            return _series.OrderByDescending(s => s.AverageRating).Take(5);
        }

        public IEnumerable<Series> GetPopular()
        {
            return _series.OrderByDescending(s => s.Chapters.Count).Take(5);
        }

        public IEnumerable<Series> GetRecentlyUpdated()
        {
            return _series.OrderByDescending(s => s.ReleaseDate).Take(5);
        }
    }
}