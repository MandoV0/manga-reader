-- ======================
-- ENUM TYPES
-- ======================
CREATE TYPE publication_status AS ENUM ('ongoing', 'completed', 'hiatus', 'cancelled');
CREATE TYPE image_type AS ENUM ('cover', 'banner', 'poster');
CREATE TYPE manga_role AS ENUM ('story', 'art');
CREATE TYPE user_role AS ENUM ('user', 'member', 'moderator', 'admin');
CREATE TYPE library_status AS ENUM ('reading', 'on_hold', 'plan_to_read', 'dropped', 'completed');
CREATE TYPE group_role AS ENUM ('leader', 'member');

-- ======================
-- CORE TABLES
-- ======================
CREATE TABLE Manga (
    mangaId BIGSERIAL PRIMARY KEY,
    title VARCHAR(255) NOT NULL,
    description TEXT,
    originalLanguage CHAR(2) NOT NULL,
    publicationStatus publication_status NOT NULL,
    releaseYear INT,
    createdAt TIMESTAMPTZ DEFAULT NOW(),
    updatedAt TIMESTAMPTZ DEFAULT NOW()
);

CREATE TABLE MangaAlternateTitle (
    titleId BIGSERIAL PRIMARY KEY,
    mangaId BIGINT REFERENCES Manga(mangaId) ON DELETE CASCADE,
    language CHAR(2) NOT NULL,
    title VARCHAR(255) NOT NULL,
    UNIQUE(mangaId, language)
);

CREATE TABLE MangaImage (
    imageId BIGSERIAL PRIMARY KEY,
    mangaId BIGINT REFERENCES Manga(mangaId) ON DELETE CASCADE,
    imageUrl VARCHAR(1024) NOT NULL,
    imageType image_type NOT NULL DEFAULT 'cover'
);

-- ======================
-- PEOPLE TABLES
-- ======================
CREATE TABLE Author (
    authorId BIGSERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL UNIQUE
);

CREATE TABLE Genre (
    genreId BIGSERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL UNIQUE
);

CREATE TABLE Tag (
    tagId BIGSERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL UNIQUE
);

CREATE TABLE ScanlationGroup (
    groupId BIGSERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL UNIQUE,
    description TEXT,
    websiteUrl VARCHAR(1024),
    createdAt TIMESTAMPTZ DEFAULT NOW()
);

CREATE TABLE Users (
    userId BIGSERIAL PRIMARY KEY,
    username VARCHAR(255) NOT NULL UNIQUE,
    email VARCHAR(255) NOT NULL UNIQUE,
    passwordHash VARCHAR(255) NOT NULL,
    role user_role NOT NULL DEFAULT 'user',
    createdAt TIMESTAMPTZ DEFAULT NOW()
);

-- ======================
-- CHAPTER TABLES
-- ======================
CREATE TABLE Chapter (
    chapterId BIGSERIAL PRIMARY KEY,
    mangaId BIGINT REFERENCES Manga(mangaId) ON DELETE CASCADE,
    volumeNumber INT,
    chapterNumber DECIMAL(6,2) NOT NULL,
    canonicalTitle VARCHAR(255),
    UNIQUE(mangaId, chapterNumber)
);

CREATE TABLE ChapterUpload (
    uploadId BIGSERIAL PRIMARY KEY,
    chapterId BIGINT REFERENCES Chapter(chapterId) ON DELETE CASCADE,
    groupId BIGINT REFERENCES ScanlationGroup(groupId) ON DELETE SET NULL,
    uploaderId BIGINT REFERENCES Users(userId) ON DELETE SET NULL,
    language CHAR(2) NOT NULL,
    publishedAt TIMESTAMPTZ DEFAULT NOW(),
    UNIQUE(chapterId, groupId, language)
);

CREATE TABLE ChapterPage (
    pageId BIGSERIAL PRIMARY KEY,
    uploadId BIGINT REFERENCES ChapterUpload(uploadId) ON DELETE CASCADE,
    pageNumber INT NOT NULL,
    imageUrl VARCHAR(1024) NOT NULL,
    UNIQUE(uploadId, pageNumber)
);

-- ======================
-- RELATION TABLES
-- ======================
CREATE TABLE MangaAuthorRelation (
    mangaId BIGINT REFERENCES Manga(mangaId) ON DELETE CASCADE,
    authorId BIGINT REFERENCES Author(authorId) ON DELETE CASCADE,
    role manga_role NOT NULL DEFAULT 'story',
    PRIMARY KEY(mangaId, authorId, role)
);

CREATE TABLE MangaGenreRelation (
    mangaId BIGINT REFERENCES Manga(mangaId) ON DELETE CASCADE,
    genreId BIGINT REFERENCES Genre(genreId) ON DELETE CASCADE,
    PRIMARY KEY(mangaId, genreId)
);

CREATE TABLE MangaTagRelation (
    mangaId BIGINT REFERENCES Manga(mangaId) ON DELETE CASCADE,
    tagId BIGINT REFERENCES Tag(tagId) ON DELETE CASCADE,
    PRIMARY KEY(mangaId, tagId)
);

-- ======================
-- USER TABLES
-- ======================
CREATE TABLE GroupMember (
    groupId BIGINT REFERENCES ScanlationGroup(groupId) ON DELETE CASCADE,
    userId BIGINT REFERENCES Users(userId) ON DELETE CASCADE,
    groupRole group_role,
    PRIMARY KEY(groupId, userId)
);

CREATE TABLE UserLibrary (
    userId BIGINT REFERENCES Users(userId) ON DELETE CASCADE,
    mangaId BIGINT REFERENCES Manga(mangaId) ON DELETE CASCADE,
    status library_status NOT NULL,
    updatedAt TIMESTAMPTZ DEFAULT NOW(),
    PRIMARY KEY(userId, mangaId)
);

CREATE TABLE UserMangaRating (
    userId BIGINT REFERENCES Users(userId) ON DELETE CASCADE,
    mangaId BIGINT REFERENCES Manga(mangaId) ON DELETE CASCADE,
    rating INT CHECK (rating >= 1 AND rating <= 10),
    review VARCHAR(2000),
    createdAt TIMESTAMPTZ DEFAULT NOW(),
    PRIMARY KEY(userId, mangaId)
);

CREATE TABLE UserChapterRating (
    userId BIGINT REFERENCES Users(userId) ON DELETE CASCADE,
    uploadId BIGINT REFERENCES ChapterUpload(uploadId) ON DELETE CASCADE,
    rating INT CHECK (rating >= 1 AND rating <= 10),
    review VARCHAR(2000),
    createdAt TIMESTAMPTZ DEFAULT NOW(),
    PRIMARY KEY(userId, uploadId)
);

CREATE TABLE ReadingHistory (
    historyId BIGSERIAL PRIMARY KEY,
    userId BIGINT REFERENCES Users(userId) ON DELETE CASCADE,
    uploadId BIGINT REFERENCES ChapterUpload(uploadId) ON DELETE CASCADE,
    lastReadAt TIMESTAMPTZ DEFAULT NOW(),
    UNIQUE(userId, uploadId)
);
