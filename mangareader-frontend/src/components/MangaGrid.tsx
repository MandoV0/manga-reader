"use client";

import { useState, useEffect, useCallback } from "react";
import MangaCard from "@/components/MangaCard";

type Manga = {
  id: number;
  title: string;
  coverImageUrl: string;
};

type PaginatedResponse = {
  items: Manga[];
  page: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
};

/**
 * MangaGrid component displays a paginated grid of manga cards.
 */
export default function MangaGrid({ initialPage = 1, pageSize = 20 }) {
  const [page, setPage] = useState(initialPage);
  const [data, setData] = useState<PaginatedResponse | null>(null);
  const [loading, setLoading] = useState(false);

  const fetchMangas = useCallback(async (pageNumber: number) => {
    setLoading(true);
    const res = await fetch(
      `http://localhost:5110/api/v1/Series?page=${pageNumber}&pageSize=${pageSize}`
    );
    const json = await res.json();
    setData(json);
    setLoading(false);
  }, [pageSize]);

  useEffect(() => {
    fetchMangas(page);
  }, [page, fetchMangas]);

  if (loading) return <p className="p-6 text-white">Loading...</p>;
  if (!data || !data.items.length) return <p className="p-6 text-white">No mangas found.</p>;

  return (
    <div className="p-6">
      <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-5 gap-4">
        {data.items.map((m) => (
          <MangaCard key={m.id} id={m.id} title={m.title} coverUrl={m.coverImageUrl} />
        ))}
      </div>

      <div className="flex justify-center items-center gap-4 mt-6 text-white">
        <button
          className="px-3 py-1 bg-zinc-800 rounded hover:bg-zinc-700 disabled:opacity-50"
          disabled={!data.hasPreviousPage}
          onClick={() => setPage((p) => Math.max(p - 1, 1))}
        >
          Previous
        </button>

        <span>
          Page {data.page} / {data.totalPages}
        </span>

        <button
          className="px-3 py-1 bg-zinc-800 rounded hover:bg-zinc-700 disabled:opacity-50"
          disabled={!data.hasNextPage}
          onClick={() => setPage((p) => p + 1)}
        >
          Next
        </button>
      </div>
    </div>
  );
}
