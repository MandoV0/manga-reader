import express from 'express';
import dotenv from 'dotenv';
import { Pool } from 'pg';
import mangaRoutes from "./routes/manga.routes"

dotenv.config();

const app = express();
app.use(express.json());

const pool = new Pool({
    host: process.env.DB_HOST,
    port: Number(process.env.DB_PORT),
    user: process.env.DB_USER,
    password: process.env.DB_PASSWORD,
    database: process.env.DB_NAME,
});

app.use("/manga", mangaRoutes);

export default app;