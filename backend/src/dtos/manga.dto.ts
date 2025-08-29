import { z } from "zod";

// For query params (list manga)
export const ListMangaQueryDto = z.object({
  status: z.enum(["ongoing", "completed", "hiatus", "cancelled"]).optional(),
  genres: z.string().optional(), // comma-separated
  tags: z.string().optional(),
  year: z.coerce.number().int().min(1900).max(new Date().getFullYear()).optional(),
  author: z.string().optional(),
  title: z.string().optional(),
  page: z.coerce.number().int().min(1).default(1),
  limit: z.coerce.number().int().min(1).max(100).default(20),
  offset: z.coerce.number().int().min(0).optional(),
});

export type ListMangaQuery = z.infer<typeof ListMangaQueryDto>;