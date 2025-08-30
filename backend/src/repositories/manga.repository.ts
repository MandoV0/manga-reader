import { Manga } from "../models/manga.model";
import { pool } from "../config/db";
import { ListMangaQuery } from "../dtos/manga.dto";
import { Image } from "../models/image.model";
import { Author } from "../models/author.model";
import { Genre } from "../models/genre.model";

export class MangaRepository {
  async findAll(
    query: ListMangaQuery
  ): Promise<{ total: number; manga: Manga[] }> {
    const { status, limit, offset } = query;

    const countRes = await pool.query(`SELECT COUNT(*) FROM manga`);
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

  async findGenres(mangaId: number): Promise<Genre[]> {
    const query = `SELECT g.genreId, name FROM Genre g LEFT JOIN MangaGenreRelation mgr on g.genreId = mgr.genreId WHERE mgr.mangaId = $1`;
    const result = await pool.query(query, [mangaId]);
    return result.rows;
  }

  async findImages(mangaId: number): Promise<Image[]> {
    const query = `SELECT * FROM mangaimage WHERE mangaId = $1`;
    const result = await pool.query<Image>(query, [mangaId]);
    return result.rows;
  }

  async findAuthors(mangaId: number): Promise<Author[]> {
    const query = `SELECT * FROM author a LEFT JOIN mangaauthorrelation mar ON mar.authorId = a.authorId WHERE mar.mangaId = $1`;
    const result = await pool.query<Author>(query, [mangaId]);
    return result.rows;
  }
}