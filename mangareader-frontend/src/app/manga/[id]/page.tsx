"use client";
import { useState, useEffect, use } from "react";
import MangaRating from "@/components/MangaRating";
import { fetchSeriesById, fetchChapters, getLastReadChapterForSeries } from "@/lib/api";
import Navbar from "@/components/Navbar";
import { SeriesDto, ChapterDto } from "@/lib/api";
import { Play, BookOpen, Calendar, User, Building } from "lucide-react";
import Link from "next/link";
import { useRouter } from "next/navigation";

type Props = { params: Promise<{ id: string }> };

export default function MangaDetail({ params }: Props) {
  const router = useRouter();
  const [manga, setManga] = useState<SeriesDto | null>(null);
  const [chapters, setChapters] = useState<ChapterDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [lastReadChapter, setLastReadChapter] = useState<any>(null);
  
  const resolvedParams = use(params);

  useEffect(() => {
    loadData();
  }, [resolvedParams.id]);

  const loadData = async () => {
    try {
      setLoading(true);
      const [mangaData, chaptersData, lastReadData] = await Promise.all([
        fetchSeriesById(Number(resolvedParams.id)),
        fetchChapters(Number(resolvedParams.id)),
        getLastReadChapterForSeries(Number(resolvedParams.id))
      ]);
      setManga(mangaData);
      setChapters(chaptersData);
      setLastReadChapter(lastReadData);
    } catch (error) {
      console.error("Error loading manga data:", error);
    } finally {
      setLoading(false);
    }
  };

  if (loading) {
    return (
      <div className="min-h-screen bg-black">
        <Navbar />
        <div className="pt-20 flex items-center justify-center">
          <div className="text-center">
            <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-red-500 mx-auto mb-4"></div>
            <p className="text-white">Loading manga...</p>
          </div>
        </div>
      </div>
    );
  }

  if (!manga) {
    return (
      <div className="min-h-screen bg-black">
        <Navbar />
        <div className="pt-20 flex items-center justify-center">
          <div className="text-center">
            <p className="text-white text-xl mb-4">Manga not found.</p>
            <button
              onClick={() => router.push("/")}
              className="netflix-gradient text-white px-6 py-3 rounded-lg"
            >
              Back to Home
            </button>
          </div>
        </div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-black">
      <Navbar />
      
      <div className="pt-20">
        <div className="relative h-[70vh] min-h-[500px] mb-8">
          <div 
            className="absolute inset-0 bg-cover bg-center bg-no-repeat"
            style={{ backgroundImage: `url(${manga.bannerImageUrl || manga.coverImageUrl})` }}
          />
          <div className="absolute inset-0 bg-gradient-to-r from-black via-black/70 to-transparent" />
          
          <div className="relative z-10 container mx-auto px-6 h-full flex items-center">
            <div className="max-w-6xl w-full">
              <div className="flex flex-col lg:flex-row gap-8 items-center lg:items-start">
                <div className="flex-shrink-0">
                  <img
                    src={manga.coverImageUrl}
                    alt={manga.title}
                    className="w-48 h-72 object-cover rounded-lg shadow-2xl"
                  />
                </div>
                
                <div className="flex-1 min-w-0">
                  <h1 className="text-3xl md:text-4xl lg:text-5xl font-bold text-white mb-4 break-words">
                    {manga.title}
                  </h1>
                  
                  <div className="flex flex-wrap items-center gap-4 mb-4">
                    <div className="flex items-center space-x-1 text-gray-300">
                      <Calendar className="h-4 w-4" />
                      <span className="text-sm">
                        {new Date(manga.releaseDate).getFullYear()}
                      </span>
                    </div>
                    
                    {manga.author && (
                      <div className="flex items-center space-x-1 text-gray-300">
                        <User className="h-4 w-4" />
                        <span className="text-sm">{manga.author}</span>
                      </div>
                    )}
                    
                    {manga.publisher && (
                      <div className="flex items-center space-x-1 text-gray-300">
                        <Building className="h-4 w-4" />
                        <span className="text-sm">{manga.publisher}</span>
                      </div>
                    )}
                    
                    <div className="flex items-center space-x-1">
                      <div className="flex text-yellow-400">
                        {Array.from({ length: 5 }, (_, i) => (
                          <span key={i} className="text-sm">
                            {i < Math.floor(manga.averageRating || 0) ? '★' : '☆'}
                          </span>
                        ))}
                      </div>
                      <span className="text-sm text-gray-300">
                        {manga.averageRating?.toFixed(1) || 'N/A'}
                      </span>
                    </div>
                  </div>
                  
                  <p className="text-gray-300 mb-6 leading-relaxed text-sm md:text-base line-clamp-3">
                    {manga.description}
                  </p>
                  
                  <div className="flex flex-wrap gap-2 mb-6">
                    {manga.genres?.map((genre, index) => (
                      <span
                        key={index}
                        className="px-3 py-1 bg-red-600/20 text-red-400 rounded-full text-sm border border-red-600/30"
                      >
                        {genre}
                      </span>
                    ))}
                  </div>
                  
                  <div className="flex flex-wrap items-center gap-4">
                    <button 
                      onClick={() => {
                        if (chapters.length > 0) {
                          const targetChapter = lastReadChapter ? lastReadChapter.chapterId : chapters[0].id;
                          router.push(`/manga/${manga.id}/chapter/${targetChapter}`);
                        }
                      }}
                      className="netflix-gradient text-white px-6 py-3 rounded-lg flex items-center space-x-2 hover:opacity-90 transition-opacity font-semibold text-sm md:text-base"
                    >
                      <Play className="h-5 w-5 fill-current" />
                      <span>{lastReadChapter ? "Continue Reading" : "Start Reading"}</span>
                    </button>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>

        <div className="container mx-auto px-6 pb-8">
          <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
            <div className="lg:col-span-2">
              <div className="bg-gray-800/50 rounded-lg p-6">
                <div className="flex items-center space-x-2 mb-6">
                  <BookOpen className="h-6 w-6 text-red-500" />
                  <h2 className="text-2xl font-bold text-white">Chapters</h2>
                </div>
                
                <div className="space-y-2">
                  {chapters.map((chapter) => (
                    <Link
                      key={chapter.id}
                      href={`/manga/${manga.id}/chapter/${chapter.id}`}
                      className="flex items-center justify-between p-4 bg-gray-700/50 rounded-lg hover:bg-gray-700 transition-colors group"
                    >
                      <div>
                        <h3 className="text-white font-medium group-hover:text-red-400 transition-colors">
                          {chapter.title || `Chapter ${chapter.id}`}
                        </h3>
                        <p className="text-sm text-gray-400">
                          {chapter.pagesCount} pages • {chapter.viewsCount} views
                        </p>
                      </div>
                      <Play className="h-5 w-5 text-gray-400 group-hover:text-red-400 transition-colors" />
                    </Link>
                  ))}
                </div>
              </div>
            </div>

            <div>
              <div className="bg-gray-800/50 rounded-lg p-6">
                <MangaRating seriesId={manga.id} />
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}