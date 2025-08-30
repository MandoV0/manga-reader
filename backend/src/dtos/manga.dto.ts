export interface ListMangaQuery {
  status?: "ongoing" | "completed" | "hiatus" | "cancelled";
  genres?: string; // comma-separated
  tags?: string;
  year?: number;
  author?: string;
  title?: string;
  limit: number;
  offset?: number;
}