import { Manga } from "../models/manga.model";
import { pool } from "../config/db";
import { ListMangaQuery } from "../dtos/manga.dto";

export class MangaRepository {
  async findAll(
    query: ListMangaQuery
  ): Promise<{ total: number; manga: Manga[] }> {
    const { status, page, limit, offset } =
      query;

    const countRes = await pool.query(`SELECT COUNT(*) FROM manga`)
    const total = parseInt(countRes.rows[0].count, 10);

    const res = await pool.query<Manga>(
      `SELECT * 
       FROM manga
       ORDER BY mangaId
       LIMIT $1 OFFSET $2`,
      [limit, offset]
    );
    return { total, manga: res.rows };
  }

  async findById(mangaId: number): Promise<Manga | null> {
    const res = await pool.query<Manga>(
      `SELECT *
       FROM manga
       WHERE mangaId = $1`,
      [mangaId]
    );

    return res.rows[0] || null;
  }
}
