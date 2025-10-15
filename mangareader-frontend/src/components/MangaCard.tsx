"use client";

import { useRouter } from "next/navigation";
import { useState } from "react";
import { Play } from "lucide-react";
import { trackMangaView } from "@/lib/api";

/**
 * MangaCard component displays a manga series with its cover and title.
 * Clicking on the card navigates to the series detail page.
 */
export default function MangaCard({ id, title, coverUrl }: { id: number; title: string; coverUrl: string }) {
  const router = useRouter();
  const [imageLoaded, setImageLoaded] = useState(false);

  const handleClick = async () => {
    await trackMangaView(id);
    router.push(`/manga/${id}`);
  };

  return (
    <div
      className="min-w-[200px] cursor-pointer group"
      onClick={handleClick}
    >
      <div className="relative overflow-hidden rounded-lg shadow-lg">
        <img
          src={coverUrl}
          alt={title}
          className={`w-full h-72 object-cover transition-all duration-300 group-hover:scale-110 ${
            imageLoaded ? 'opacity-100' : 'opacity-0'
          }`}
          onLoad={() => setImageLoaded(true)}
        />
        
        <div className="absolute inset-0 bg-gradient-to-t from-black/80 via-transparent to-transparent opacity-0 group-hover:opacity-100 transition-opacity duration-300" />
        
        <div className="absolute bottom-0 left-0 right-0 p-4 text-white transform translate-y-full group-hover:translate-y-0 transition-transform duration-300">
          <h3 className="font-semibold text-sm mb-2 line-clamp-2">{title}</h3>
          <button 
            onClick={(e) => {
              e.stopPropagation();
              handleClick();
            }}
            className="w-full bg-white text-black px-3 py-2 rounded text-xs font-medium hover:bg-gray-200 transition-colors flex items-center justify-center space-x-1"
          >
            <Play className="h-3 w-3 fill-current" />
            <span>Read</span>
          </button>
        </div>
      </div>
    </div>
  );
}