"use client";
import { useState, useEffect } from "react";
import { fetchAllSeries, SeriesListDto, PagedResponseDto } from "@/lib/api";
import Navbar from "@/components/Navbar";
import MangaCard from "@/components/MangaCard";
import { Search, Filter, Grid, List } from "lucide-react";

export default function BrowsePage() {
  const [series, setSeries] = useState<SeriesListDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [sortBy, setSortBy] = useState("");
  const [viewMode, setViewMode] = useState<"grid" | "list">("grid");

  useEffect(() => {
    loadSeries();
  }, [currentPage, sortBy]);

  const loadSeries = async () => {
    try {
      setLoading(true);
      const response: PagedResponseDto<SeriesListDto> = await fetchAllSeries(currentPage, 20, sortBy);
      setSeries(response.items);
      setTotalPages(response.totalPages);
    } catch (error) {
      console.error("Failed to load series:", error);
    } finally {
      setLoading(false);
    }
  };

  const handlePageChange = (page: number) => {
    setCurrentPage(page);
    window.scrollTo({ top: 0, behavior: 'smooth' });
  };

  return (
    <div className="min-h-screen">
      <Navbar />
      
      <div className="pt-20 pb-8">
        <div className="container mx-auto px-6">
          <div className="flex items-center justify-between mb-8">
            <div>
              <h1 className="text-3xl font-bold text-white mb-2">Browse All Manga</h1>
              <p className="text-gray-400">Discover your next favorite series</p>
            </div>
            
            <div className="flex items-center space-x-4">
              <select
                value={sortBy}
                onChange={(e) => setSortBy(e.target.value)}
                className="bg-gray-800 border border-gray-600 text-white px-4 py-2 rounded-lg focus:border-red-500 focus:outline-none"
              >
                <option value="">Sort by</option>
                <option value="title">Title A-Z</option>
                <option value="title_desc">Title Z-A</option>
                <option value="rating">Highest Rated</option>
                <option value="updated">Recently Updated</option>
                <option value="release">Newest First</option>
              </select>
              
              <div className="flex bg-gray-800 rounded-lg p-1">
                <button
                  onClick={() => setViewMode("grid")}
                  className={`p-2 rounded ${viewMode === "grid" ? "bg-red-600 text-white" : "text-gray-400 hover:text-white"}`}
                >
                  <Grid className="h-4 w-4" />
                </button>
                <button
                  onClick={() => setViewMode("list")}
                  className={`p-2 rounded ${viewMode === "list" ? "bg-red-600 text-white" : "text-gray-400 hover:text-white"}`}
                >
                  <List className="h-4 w-4" />
                </button>
              </div>
            </div>
          </div>

          {loading ? (
            <div className="flex items-center justify-center py-12">
              <div className="animate-spin rounded-full h-8 w-8 border-b-2 border-red-500"></div>
              <span className="ml-2 text-white">Loading...</span>
            </div>
          ) : (
            <>
              <div className={`grid gap-6 ${
                viewMode === "grid" 
                  ? "grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5" 
                  : "grid-cols-1"
              }`}>
                {series.map((manga) => (
                  <MangaCard
                    key={manga.id}
                    id={manga.id}
                    title={manga.title}
                    coverUrl={manga.coverImageUrl}
                  />
                ))}
              </div>

              {totalPages > 1 && (
                <div className="flex items-center justify-center mt-12">
                  <div className="flex items-center space-x-2">
                    <button
                      onClick={() => handlePageChange(currentPage - 1)}
                      disabled={currentPage === 1}
                      className="px-4 py-2 bg-gray-800 text-white rounded-lg hover:bg-gray-700 disabled:opacity-50 disabled:cursor-not-allowed transition-colors"
                    >
                      Previous
                    </button>
                    
                    <div className="flex items-center space-x-1">
                      {Array.from({ length: Math.min(5, totalPages) }, (_, i) => {
                        const pageNum = currentPage <= 3 ? i + 1 : currentPage - 2 + i;
                        if (pageNum > totalPages) return null;
                        
                        return (
                          <button
                            key={pageNum}
                            onClick={() => handlePageChange(pageNum)}
                            className={`px-3 py-2 rounded-lg transition-colors ${
                              currentPage === pageNum
                                ? "bg-red-600 text-white"
                                : "bg-gray-800 text-white hover:bg-gray-700"
                            }`}
                          >
                            {pageNum}
                          </button>
                        );
                      })}
                    </div>
                    
                    <button
                      onClick={() => handlePageChange(currentPage + 1)}
                      disabled={currentPage === totalPages}
                      className="px-4 py-2 bg-gray-800 text-white rounded-lg hover:bg-gray-700 disabled:opacity-50 disabled:cursor-not-allowed transition-colors"
                    >
                      Next
                    </button>
                  </div>
                </div>
              )}
            </>
          )}
        </div>
      </div>
    </div>
  );
}