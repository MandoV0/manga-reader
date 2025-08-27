# Database Schema
This repo contains the PostgreSQL database schema for our manga reading platform.
It is designed to support multiple scanlation groups, multilingual chapters, user libraries, ratings and more.
The Schema is ready to be used in the Dockerized development enviroment.

# Features
- Core Content
    Manga, Chapter, ChapterUpload, ChapterPage
    Supports multiple translations and scanlation groups per chapter
    Handles covers, banners and posters with MangaImage
    Alternative titles for multiple languages
- People
    Author, Genre, Tag
    Many-to-Many relations between manga, authors, genres and tags
    Scanlation groups and members GroupMember
- User System
    Users (Users) with roles (user, member, moderator, admin)
    Library/bookmarks (UserLibrary)
    Ratings (UserRating)
    Reading history tracking (ReadingHistory)
- Modern Design
    Timestamp fields (createdAt, updatedAt) for tracking
    Proper ON DELETE rules for cascading deletions
    Enums for roles, statuses, and types
    Composite unique constraints to prevent duplicate entries


```bash
docker-compose down -v   # deletes containers + volumes
docker-compose up -d     # recreates fresh DB and runs init scripts

docker-compose exec postgres psql -U postgres -d mydb # To connect to the database
```