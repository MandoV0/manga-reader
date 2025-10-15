"use client";
import { useState, useEffect } from "react";
import { fetchSeries, SeriesListDto } from "@/lib/api";
import Navbar from "@/components/Navbar";
import MangaCard from "@/components/MangaCard";
import { TrendingUp, Clock } from "lucide-react";

export default function TrendingPage() {
  const [trending, setTrending] = useState<SeriesListDto[]>([]);
  const [popular, setPopular] = useState<SeriesListDto[]>([]);
  const [recent, setRecent] = useState<SeriesListDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [activeTab, setActiveTab] = useState<"trending" | "popular" | "recent">("trending");

  useEffect(() => {
    loadData();
  }, []);

  const loadData = async () => {
    try {
      setLoading(true);
      const [trendingData, popularData, recentData] = await Promise.all([
        fetchSeries("trending"),
        fetchSeries("popular"),
        fetchSeries("recently-updated")
      ]);
      
      setTrending(trendingData);
      setPopular(popularData);
      setRecent(recentData);
    } catch (error) {
      console.error("Failed to load data:", error);
    } finally {
      setLoading(false);
    }
  };

  const getCurrentData = () => {
    switch (activeTab) {
      case "trending": return trending;
      case "popular": return popular;
      case "recent": return recent;
      default: return trending;
    }
  };

  const getTabTitle = () => {
    switch (activeTab) {
      case "trending": return "Trending Now";
      case "popular": return "Popular This Week";
      case "recent": return "Recently Updated";
      default: return "Trending Now";
    }
  };

  const getTabDescription = () => {
    switch (activeTab) {
      case "trending": return "Most viewed manga in the last 24 hours";
      case "popular": return "Most popular manga based on chapter count";
      case "recent": return "Recently updated manga series";
      default: return "Most viewed manga in the last 24 hours";
    }
  };

  return (
    <div className="min-h-screen">
      <Navbar />
      
      <div className="pt-20 pb-8">
        <div className="container mx-auto px-6">
          <div className="mb-8">
            <h1 className="text-3xl font-bold text-white mb-2">Trending & Popular</h1>
            <p className="text-gray-400">Discover what's hot right now</p>
          </div>

          <div className="flex space-x-1 mb-8 bg-gray-800/50 rounded-lg p-1 w-fit">
            <button
              onClick={() => setActiveTab("trending")}
              className={`flex items-center space-x-2 px-4 py-2 rounded-lg transition-colors ${
                activeTab === "trending"
                  ? "bg-red-600 text-white"
                  : "text-gray-400 hover:text-white"
              }`}
            >
              <TrendingUp className="h-4 w-4" />
              <span>Trending</span>
            </button>
            
            <button
              onClick={() => setActiveTab("popular")}
              className={`flex items-center space-x-2 px-4 py-2 rounded-lg transition-colors ${
                activeTab === "popular"
                  ? "bg-red-600 text-white"
                  : "text-gray-400 hover:text-white"
              }`}
            >
              <TrendingUp className="h-4 w-4" />
              <span>Popular</span>
            </button>
            
            <button
              onClick={() => setActiveTab("recent")}
              className={`flex items-center space-x-2 px-4 py-2 rounded-lg transition-colors ${
                activeTab === "recent"
                  ? "bg-red-600 text-white"
                  : "text-gray-400 hover:text-white"
              }`}
            >
              <Clock className="h-4 w-4" />
              <span>Recent</span>
            </button>
          </div>

          <div className="mb-6">
            <h2 className="text-2xl font-bold text-white mb-2">{getTabTitle()}</h2>
            <p className="text-gray-400">{getTabDescription()}</p>
          </div>

          {loading ? (
            <div className="flex items-center justify-center py-12">
              <div className="animate-spin rounded-full h-8 w-8 border-b-2 border-red-500"></div>
              <span className="ml-2 text-white">Loading...</span>
            </div>
          ) : getCurrentData().length > 0 ? (
            <div className="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 gap-6">
              {getCurrentData().map((manga) => (
                <MangaCard
                  key={manga.id}
                  id={manga.id}
                  title={manga.title}
                  coverUrl={manga.coverImageUrl}
                />
              ))}
            </div>
          ) : (
            <div className="text-center py-12">
              <TrendingUp className="h-16 w-16 text-gray-500 mx-auto mb-4" />
              <h3 className="text-xl font-semibold text-white mb-2">No trending manga yet</h3>
              <p className="text-gray-400 mb-6">
                {activeTab === "trending" 
                  ? "Start viewing manga to see what's trending!"
                  : "No manga available at the moment."
                }
              </p>
              {activeTab === "trending" && (
                <a
                  href="/browse"
                  className="netflix-gradient text-white px-6 py-3 rounded-lg hover:opacity-90 transition-opacity inline-block"
                >
                  Browse All Manga
                </a>
              )}
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
