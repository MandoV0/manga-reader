import { Manga } from "../models/manga.model";
import { pool } from "../config/db";
import { ListMangaQuery, MangaDetailsDto } from "../dtos/manga.dto";
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
      `SELECT m.mangaId, m.title, m.description, m.publicationStatus, m.releaseYear, mi.imageUrl as coverUrl 
      FROM manga m
      LEFT JOIN mangaimage mi ON m.mangaId = mi.mangaId AND mi.imagetype = 'cover'
      ORDER BY mangaId
      LIMIT $1 OFFSET $2`,
      [limit, offset]
    );
    return { total, manga: res.rows };
  }

  async findById(mangaId: number): Promise<MangaDetailsDto | null> {
    const res = await pool.query<MangaDetailsDto>(
      `SELECT
      m.mangaId,
      m.title,
      m.originallanguage,
      m.publicationstatus,
      m.releaseYear,
      m.description,
      COALESCE(
        (SELECT json_agg(json_build_object(
            'imageid', mi.imageid,
            'imageurl', mi.imageurl,
            'imagetype', mi.imagetype
          ))
        FROM mangaimage mi
        WHERE mi.mangaid = m.mangaId
        ), '[]') AS images,
      COALESCE(
        (SELECT json_agg(json_build_object(
            'authorid', a.authorid,
            'name', a.name,
            'role', mar.role
          ))
        FROM mangaauthorrelation mar
        JOIN author a ON a.authorid = mar.authorid
        WHERE mar.mangaid = m.mangaId
        ), '[]') AS authors,
      COALESCE(
        (SELECT json_agg(json_build_object(
            'genreid', g.genreid,
            'name', g.name
          ))
        FROM mangagenrerelation mgr
        JOIN genre g ON g.genreid = mgr.genreid
        WHERE mgr.mangaid = m.mangaId
        ), '[]') AS genres
    FROM manga m
    WHERE m.mangaId = $1;
    `,
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

  async findCoverUrl(mangaId: number): Promise<string> {
    const query = `SELECT imageurl as coverUrl FROM mangaimage WHERE imagetype = 'cover' AND mangaId = $1`;
    const result = await pool.query(query, [mangaId]);
    return result.rows[0]?.coverUrl || "";
  }
}
