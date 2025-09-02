import { Author } from "../models/author.model";
import { Genre } from "../models/genre.model";
import { ImageDto } from "../models/image.model";
import { Manga } from "../models/manga.model";

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

export interface MangaDetailsDto extends Manga {
  images?: ImageDto[];
  authors?: Author[];
  genres?: Genre[];
}