"use client";
import { useState } from "react";
import { Play, Info } from "lucide-react";
import Link from "next/link";
import { SeriesListDto, trackMangaView } from "@/lib/api";

interface HeroSectionProps {
  featuredManga: SeriesListDto;
}

/**
 * HeroSection component displays a featured manga in large at the Top of the landing page.
 */
export default function HeroSection({ featuredManga }: HeroSectionProps) {
  const [imageLoaded, setImageLoaded] = useState(false);

  const handleMangaClick = async () => {
    await trackMangaView(featuredManga.id);
  };

  if (!featuredManga) {
    return (
      <section className="relative h-screen flex items-center justify-center">
        <div className="text-center">
          <h1 className="text-6xl font-bold text-white mb-4">Welcome to MangaReader</h1>
          <p className="text-xl text-gray-300">Discover amazing manga stories</p>
        </div>
      </section>
    );
  }

  return (
    <section className="relative h-screen overflow-hidden">
      <div className="absolute inset-0 z-0">
        <img
          src={featuredManga.coverImageUrl}
          alt={featuredManga.title}
          className={`w-full h-full object-cover transition-opacity duration-500 ${
            imageLoaded ? 'opacity-100' : 'opacity-0'
          }`}
          onLoad={() => setImageLoaded(true)}
        />
        <div className="absolute inset-0 bg-gradient-to-r from-black via-black/50 to-transparent" />
        <div className="absolute inset-0 bg-gradient-to-t from-black via-transparent to-transparent" />
      </div>

      <div className="relative z-10 h-full flex items-center">
        <div className="container mx-auto px-6">
          <div className="max-w-2xl">
            <h1 className="text-6xl font-bold text-white mb-4 leading-tight">
              {featuredManga.title}
            </h1>
            <p className="text-xl text-gray-300 mb-8 leading-relaxed">
              Discover the latest chapters and immerse yourself in this captivating story.
            </p>
            
            <div className="flex items-center space-x-4">
              <Link
                href={`/manga/${featuredManga.id}`}
                onClick={handleMangaClick}
                className="netflix-gradient text-white px-8 py-3 rounded-lg flex items-center space-x-2 hover:opacity-90 transition-opacity font-semibold"
              >
                <Play className="h-5 w-5 fill-current" />
                <span>Read Now</span>
              </Link>
              
              <Link
                href={`/manga/${featuredManga.id}`}
                onClick={handleMangaClick}
                className="bg-black/50 text-white px-4 py-3 rounded-full hover:bg-black/70 transition-colors border border-gray-600"
              >
                <Info className="h-6 w-6" />
              </Link>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
}
