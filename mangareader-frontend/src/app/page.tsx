import { fetchSeries } from "@/lib/api";
import MangaRow from "@/components/MangaRow";
import Navbar from "@/components/Navbar";
import HeroSection from "@/components/HeroSection";

/**
 * HomePage component displays trending, popular, and recently updated manga series and the Hero Section at the Toop of the screen.
 */
export default async function HomePage() {
  const [trending, popular, recent] = await Promise.all([
    fetchSeries("trending"),
    fetchSeries("popular"),
    fetchSeries("recently-updated")
  ]);

  // Use popular series as fallback for trending if it returns empty.
  // When no users view Mangas in the last 24 Hours it is empty.
  const trendingData = trending.length > 0 ? trending : popular;
  const featuredManga = trendingData[2] || popular[2];

  return (
    <main className="min-h-screen">
      <Navbar />
      <HeroSection featuredManga={featuredManga} />
      <div className="pt-20 pb-8">
        <MangaRow 
          title={trending.length > 0 ? "Trending Now" : "Popular This Week"} 
          mangas={trendingData} 
        />
        <MangaRow title="Popular This Week" mangas={popular} />
        <MangaRow title="Recently Updated" mangas={recent} />
      </div>
    </main>
  );
}