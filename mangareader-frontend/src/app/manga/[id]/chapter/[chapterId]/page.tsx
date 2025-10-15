"use client";
import { useState, useEffect, use } from "react";
import { useRouter } from "next/navigation";
import { fetchChapterById, updateLastReadChapter, isAuthenticated } from "@/lib/api";
import { ChapterDto } from "@/lib/api";
import Navbar from "@/components/Navbar";
import { ChevronLeft, ChevronRight, ArrowLeft, Maximize2, Minimize2 } from "lucide-react";

type Props = { 
  params: Promise<{ id: string; chapterId: string }> 
};

export default function ChapterReader({ params }: Props) {
  const router = useRouter();
  const resolvedParams = use(params);
  const seriesId = parseInt(resolvedParams.id);
  const chapterId = parseInt(resolvedParams.chapterId);
  
  const [chapter, setChapter] = useState<ChapterDto | null>(null);
  const [currentPage, setCurrentPage] = useState(0);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [isFullscreen, setIsFullscreen] = useState(false);

  useEffect(() => {
    loadChapter();
  }, [chapterId]);

  const loadChapter = async () => {
    try {
      setLoading(true);
      const chapterData = await fetchChapterById(chapterId);
      setChapter(chapterData);
      setCurrentPage(0);
      
      if (isAuthenticated()) {
        try {
          await updateLastReadChapter(seriesId, chapterId);
        } catch (error) {
          console.error("Failed to update reading history:", error);
        }
      }
    } catch (error) {
      setError("Failed to load chapter");
      console.error("Error loading chapter:", error);
    } finally {
      setLoading(false);
    }
  };

  const goToPreviousPage = () => {
    if (currentPage > 0) {
      setCurrentPage(currentPage - 1);
    }
  };

  const goToNextPage = () => {
    if (chapter && currentPage < chapter.pages.length - 1) {
      setCurrentPage(currentPage + 1);
    }
  };

  const toggleFullscreen = () => {
    setIsFullscreen(!isFullscreen);
  };

  const handleKeyPress = (e: KeyboardEvent) => {
    if (e.key === "ArrowLeft") goToPreviousPage();
    if (e.key === "ArrowRight") goToNextPage();
    if (e.key === "Escape") setIsFullscreen(false);
  };

  useEffect(() => {
    window.addEventListener("keydown", handleKeyPress);
    return () => window.removeEventListener("keydown", handleKeyPress);
  }, [currentPage]);

  if (loading) {
    return (
      <div className="min-h-screen bg-black">
        <Navbar />
        <div className="pt-20 flex items-center justify-center">
          <div className="text-center">
            <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-red-500 mx-auto mb-4"></div>
            <p className="text-white">Loading chapter...</p>
          </div>
        </div>
      </div>
    );
  }

  if (error || !chapter) {
    return (
      <div className="min-h-screen bg-black">
        <Navbar />
        <div className="pt-20 flex items-center justify-center">
          <div className="text-center">
            <p className="text-white text-xl mb-4">Chapter not found</p>
            <button
              onClick={() => router.push(`/manga/${seriesId}`)}
              className="netflix-gradient text-white px-6 py-3 rounded-lg"
            >
              Back to Manga
            </button>
          </div>
        </div>
      </div>
    );
  }

  const currentPageData = chapter.pages[currentPage];

  return (
    <div className={`min-h-screen bg-black ${isFullscreen ? 'fixed inset-0 z-50' : ''}`}>
      {!isFullscreen && <Navbar />}
      
      <div className={`${!isFullscreen ? 'pt-20' : ''}`}>
        {!isFullscreen && (
          <div className="bg-black/80 backdrop-blur-sm border-b border-gray-800">
            <div className="container mx-auto px-4 py-3">
              <div className="flex items-center justify-between">
                <button
                  onClick={() => router.push(`/manga/${seriesId}`)}
                  className="flex items-center space-x-2 text-white hover:text-red-500 transition-colors"
                >
                  <ArrowLeft className="h-5 w-5" />
                  <span>Back to Manga</span>
                </button>
                
                <div className="flex items-center space-x-4">
                  <h1 className="text-white font-semibold">
                    {chapter.title || `Chapter ${chapterId}`}
                  </h1>
                  <span className="text-gray-400">
                    {currentPage + 1} / {chapter.pages.length}
                  </span>
                </div>
                
                <button
                  onClick={toggleFullscreen}
                  className="text-white hover:text-red-500 transition-colors"
                >
                  <Maximize2 className="h-5 w-5" />
                </button>
              </div>
            </div>
          </div>
        )}

        <div className="relative">
          <div className="absolute inset-0 flex">
            <div 
              className="flex-1 cursor-pointer" 
              onClick={goToPreviousPage}
              onKeyDown={(e) => e.key === 'Enter' && goToPreviousPage()}
              tabIndex={0}
            />
            
            <div 
              className="flex-1 cursor-pointer" 
              onClick={goToNextPage}
              onKeyDown={(e) => e.key === 'Enter' && goToNextPage()}
              tabIndex={0}
            />
          </div>

          <div className="flex justify-center">
            <img
              src={currentPageData?.imageUrl}
              alt={`Page ${currentPage + 1}`}
              className="max-w-full max-h-screen object-contain"
              onError={(e) => {
                const target = e.target as HTMLImageElement;
                target.style.display = 'none';
              }}
            />
          </div>
        </div>

        {!isFullscreen && (
          <div className="bg-black/80 backdrop-blur-sm border-t border-gray-800">
            <div className="container mx-auto px-4 py-4">
              <div className="flex items-center justify-between">
                <button
                  onClick={goToPreviousPage}
                  disabled={currentPage === 0}
                  className="flex items-center space-x-2 px-4 py-2 bg-gray-800 text-white rounded-lg hover:bg-gray-700 disabled:opacity-50 disabled:cursor-not-allowed transition-colors"
                >
                  <ChevronLeft className="h-5 w-5" />
                  <span>Previous</span>
                </button>
                
                <div className="flex items-center space-x-2">
                  {chapter.pages.map((_, index) => (
                    <button
                      key={index}
                      onClick={() => setCurrentPage(index)}
                      className={`w-2 h-2 rounded-full transition-colors ${
                        index === currentPage ? 'bg-red-500' : 'bg-gray-600'
                      }`}
                    />
                  ))}
                </div>
                
                <button
                  onClick={goToNextPage}
                  disabled={currentPage === chapter.pages.length - 1}
                  className="flex items-center space-x-2 px-4 py-2 bg-gray-800 text-white rounded-lg hover:bg-gray-700 disabled:opacity-50 disabled:cursor-not-allowed transition-colors"
                >
                  <span>Next</span>
                  <ChevronRight className="h-5 w-5" />
                </button>
              </div>
            </div>
          </div>
        )}

        {isFullscreen && (
          <div className="absolute top-4 right-4">
            <button
              onClick={toggleFullscreen}
              className="bg-black/50 text-white p-2 rounded-lg hover:bg-black/70 transition-colors"
            >
              <Minimize2 className="h-5 w-5" />
            </button>
          </div>
        )}
      </div>
    </div>
  );
}
