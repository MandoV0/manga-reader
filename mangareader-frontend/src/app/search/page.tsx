"use client";
import { useState, useEffect } from "react";
import { useSearchParams } from "next/navigation";
import { searchSeries, SeriesListDto } from "@/lib/api";
import Navbar from "@/components/Navbar";
import MangaCard from "@/components/MangaCard";
import { Search, Loader2 } from "lucide-react";

export default function SearchPage() {
  const searchParams = useSearchParams();
  const query = searchParams.get('q') || '';
  
  const [searchResults, setSearchResults] = useState<SeriesListDto[]>([]);
  const [loading, setLoading] = useState(false);
  const [searchQuery, setSearchQuery] = useState(query);
  const [currentPage, setCurrentPage] = useState(1);

  useEffect(() => {
    if (query) {
      performSearch(query);
    }
  }, [query]);

  const performSearch = async (searchTerm: string) => {
    if (!searchTerm.trim()) return;
    
    setLoading(true);
    try {
      const results = await searchSeries(searchTerm, currentPage);
      setSearchResults(results.items);
    } catch (error) {
      console.error('Search failed:', error);
      setSearchResults([]);
    } finally {
      setLoading(false);
    }
  };

  const handleSearch = (e: React.FormEvent) => {
    e.preventDefault();
    if (searchQuery.trim()) {
      setCurrentPage(1);
      performSearch(searchQuery.trim());
    }
  };

  return (
    <div className="min-h-screen">
      <Navbar />
      
      <div className="pt-20 px-6">
        <div className="max-w-4xl mx-auto">
          <div className="mb-8">
            <h1 className="text-3xl font-bold text-white mb-4">Search Results</h1>
            
            <form onSubmit={handleSearch} className="relative max-w-md">
              <div className="relative">
                <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 h-5 w-5" />
                <input
                  type="text"
                  placeholder="Search for manga..."
                  value={searchQuery}
                  onChange={(e) => setSearchQuery(e.target.value)}
                  className="w-full bg-black/50 text-white placeholder-gray-400 pl-12 pr-4 py-3 rounded-lg border border-gray-600 focus:border-red-500 focus:outline-none transition-colors"
                />
              </div>
            </form>
          </div>

          {loading ? (
            <div className="flex items-center justify-center py-12">
              <Loader2 className="h-8 w-8 animate-spin text-white" />
              <span className="ml-2 text-white">Searching...</span>
            </div>
          ) : searchResults.length > 0 ? (
            <div>
              <p className="text-gray-300 mb-6">
                Found {searchResults.length} results for "{query}"
              </p>
              <div className="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 gap-6">
                {searchResults.map((manga) => (
                  <MangaCard
                    key={manga.id}
                    id={manga.id}
                    title={manga.title}
                    coverUrl={manga.coverImageUrl}
                  />
                ))}
              </div>
            </div>
          ) : query ? (
            <div className="text-center py-12">
              <Search className="h-16 w-16 text-gray-500 mx-auto mb-4" />
              <h3 className="text-xl font-semibold text-white mb-2">No results found</h3>
              <p className="text-gray-400">
                Try searching with different keywords or check your spelling.
              </p>
            </div>
          ) : (
            <div className="text-center py-12">
              <Search className="h-16 w-16 text-gray-500 mx-auto mb-4" />
              <h3 className="text-xl font-semibold text-white mb-2">Start searching</h3>
              <p className="text-gray-400">
                Enter a manga title, author, or genre to find what you're looking for.
              </p>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
