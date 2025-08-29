import { Router } from "express";
import { MangaController } from "../controllers/manga.controller";

const router = Router();
const mangaController = new MangaController();

router.get("/", mangaController.getMangas);
router.get("/:mangaId", mangaController.getMangaById);

export default router;