import { ListMangaQuery } from "../dtos/manga.dto";
import { MangaRepository } from "../repositories/manga.repository";

export class MangaService {
    private mangaRepo = new MangaRepository();

    async getMangas(query: ListMangaQuery) {
        return this.mangaRepo.findAll(query);
    }

    async getMangaById(mangaId: number) {
        const manga = await this.mangaRepo.findById(mangaId);
        if (!manga) {
            throw new Error("Manga not found");
        }
        return manga;
    }
}