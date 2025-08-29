import { Request, Response } from "express";
import { MangaService } from "../services/manga.service";
import { ListMangaQueryDto } from "../dtos/manga.dto";

export class MangaController {
  private mangaService = new MangaService();

  getMangas = async (req: Request, res: Response) => {
    try {
      const parsed = ListMangaQueryDto.parse(req.query);
      const result = await this.mangaService.getMangas(parsed);

      res.json({
        page: parsed.page,
        limit: parsed.limit,
        offset: parsed.offset ?? (parsed.page - 1) * parsed.limit,
        total: result.total,
        manga: result.manga,
      });
    } catch (err: any) {
      if (err.name === "ZodError") {
        return res.status(400).json({ error: err.errors });
      }
      console.error(err);
      res.status(500).json({ error: "Unexpected server error" });
    }
  };

  getMangaById = async (req: Request, res: Response) => {
    try {
      const mangaId = parseInt(req.params.mangaId, 10);
      if (isNaN(mangaId) || mangaId < 0) {
        return res.status(400).json({ error: "Invalid mangaId" });
      }

      const manga = await this.mangaService.getMangaById(mangaId);
      res.json(manga);
    } catch (err: any) {
      if (err.message === "Manga not found") {
        return res.status(404).json({ error: "Manga not found" });
      }
      console.error(err);
      res.status(500).json({ error: "Unexpected server error" });
    }
  };
}
