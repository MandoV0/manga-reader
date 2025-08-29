export interface Manga {
  mangaId: number;
  title: string;
  coverUrl?: string;
  publicationStatus: "ongoing" | "completed" | "hiatus" | "cancelled";
  releaseYear?: number;
  description?: string;
}