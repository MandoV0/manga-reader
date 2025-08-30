import { ListMangaQuery, MangaDetailsDto } from "../dtos/manga.dto";
import { MangaRepository } from "../repositories/manga.repository";

export class MangaService {
    private mangaRepo = new MangaRepository();

    async getMangas(query: ListMangaQuery) {
        return this.mangaRepo.findAll(query);
    }

    async getMangaById(mangaId: number): Promise<MangaDetailsDto> {
        const manga = await this.mangaRepo.findById(mangaId);
        if (!manga) throw new Error("Manga not found");
        
        const [genres, authors, images] = await Promise.all([
            this.mangaRepo.findGenres(mangaId),
            this.mangaRepo.findAuthors(mangaId),
            this.mangaRepo.findImages(mangaId)
        ]);

        return {
            ...manga,
            genres,
            authors,
            images
        };
    }
}