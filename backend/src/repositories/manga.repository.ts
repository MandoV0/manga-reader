import { Manga } from "../models/manga.model";
import { pool } from "../config/db";
import { ListMangaQuery } from "../dtos/manga.dto";

export class MangaRepository {
  async findAll(
    query: ListMangaQuery
  ): Promise<{ total: number; manga: Manga[] }> {
    const { status, genres, tags, year, author, title, page, limit, offset } =
      query;

    const conditions: string[] = [];
    const values: any[] = [];
    let i = 1;

    if (status) {
      conditions.push(`publication_status = $${i++}`);
      values.push(status);
    }
    if (year) {
      conditions.push(`release_year = $${i++}`);
      values.push(year);
    }
    if (title) {
      conditions.push(`title ILIKE $${i++}`);
      values.push(`%${title}%`);
    }

    const whereSQL = conditions.length
      ? `WHERE ${conditions.join(" AND ")}`
      : "";

    const countRes = await pool.query(
      `SELECT COUNT(*) FROM manga ${whereSQL}`,
      values
    );
    const total = parseInt(countRes.rows[0].count, 10);

    const limitValue = limit ?? 10;
    const offsetValue = limit ?? (query.page! - 1) * limitValue;

    const res = await pool.query<Manga>(
      `SELECT manga_id as "mangaId", title, cover_url as "coverUrl",
              publication_status as "publicationStatus",
              release_year as "releaseYear", description
       FROM manga
       ${whereSQL}
       ORDER BY manga_id
       LIMIT $${i++} OFFSET $${i++}`,
      [...values, limitValue, offsetValue]
    );
    return { total, manga: res.rows };
  }

  async findById(mangaId: number): Promise<Manga | null> {
    const res = await pool.query<Manga>(
      `SELECT manga_id as "mangaId", title, cover_url as "coverUrl",
              publication_status as "publicationStatus",
              release_year as "releaseYear", description
       FROM manga
       WHERE manga_id = $1`,
      [mangaId]
    );

    return res.rows[0] || null;
  }
}
