import { ListMangaQuery } from "../dtos/manga.dto";
import { Request } from "express";

export function validateMangaQuery(req: Request): {
  valid: boolean;
  errors?: string[];
  query?: ListMangaQuery;
} {
  const errors: string[] = [];
  const { status, genres, tags, year, author, title, page, limit, offset } =
    req.query;

  const validStatuses = ["ongoing", "completed", "hiatus", "cancelled"];
  if (status && !validStatuses.includes(String(status))) {
    errors.push(`Invalid status: ${status}`);
  }

  let parsedYear: number | undefined;
  if (year !== undefined) {
    const y = Number(year);
    if (isNaN(y)) {
      errors.push(`Invalid year: ${year}`);
    } else {
      parsedYear = y;
    }
  }

  const parsedLimit = limit !== undefined ? Number(limit) : 20;
  const parsedOffset = offset !== undefined ? Number(offset) : 0;

  if (isNaN(parsedLimit) || parsedLimit < 1 || parsedLimit > 100) {
    errors.push("Limit must be between 1 and 100");
  }
  if (isNaN(parsedOffset) || parsedOffset < 0) {
    errors.push("Offset must be >= 0");
  }

  if (errors.length) return { valid: false, errors };

  return {
    valid: true,
    query: {
      status: status as ListMangaQuery["status"],
      genres: genres as string | undefined,
      tags: tags as string | undefined,
      year: parsedYear,
      author: author as string | undefined,
      title: title as string | undefined,
      limit: parsedLimit,
      offset: parsedOffset,
    },
  };
}
