import { Request, Response } from "express";
import { MangaService } from "../services/manga.service";
import { ListMangaQuery } from "../dtos/manga.dto";
import { validateMangaQuery } from "../utils/validation";

export class MangaController {
  private mangaService = new MangaService();

  getMangas = async (req: Request, res: Response) => {
    try {
      const validation = validateMangaQuery(req);
      if (!validation.valid) {
        return res.status(400).json({ error: validation.errors });
      }
      const result = await this.mangaService.getMangas(validation.query!);

      res.json({
        limit: validation.query!.limit,
        offset: validation.query!.offset,
        total: result.total,
        manga: result.manga,
      });
    } catch (err: any) {
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
