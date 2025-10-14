namespace MangaReaderAPI.DTOs
{
    public class LastReadChapterDto
    {
        public int SeriesId { get; set; }
        public int ChapterId { get; set; }
        public string SeriesTitle { get; set; } = String.Empty;
        public string ChapterTitle { get; set; } = String.Empty;
        public string CoverImageUrl { get; set; } = String.Empty;
    }
}
