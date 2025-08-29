import { Pool } from "pg";

export const pool = new Pool({
  user: process.env.DB_USER || "postgres",
  host: process.env.DB_HOST || "localhost", // 'postgres' if using docker-compose
  database: process.env.DB_NAME || "mydb",
  password: process.env.DB_PASSWORD || "postgres",
  port: parseInt(process.env.DB_PORT || "5432", 10),
});