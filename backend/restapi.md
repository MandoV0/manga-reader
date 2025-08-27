# Manga-reader REST API
A RESTful API for the manga-reader, providing endpoints for browsing manga, chapters, pages, scanlation groups, users and ratings.
All responses are in JSON format.

# Table of Contents

# Authentication
- Public endpoints (browsing, reading) do not require authentication.
- Private endpoints (uploads, user library, ratings submission) require a __JWT Bearer Token__ in the Authorization Header:
```
Authorization: Bearer <token>
```

# Endpoints
## Manga
- List Manga
```
GET /manga
```
__Query Parameters:__ status, genres, tags, year, author, title, page, limit, offset
Response Example:
```json
{
  "page": 1,
  "limit": 20,
  "offset": 10,
  "total": 452,
  "manga": [
    {
      "mangaId": 1,
      "title": "One Piece",
      "coverUrl": "https://cdn.example.com/onepiece.jpg",
      "publicationStatus": "ongoing",
      "releaseYear": 1997,
      "description": "A story about pirates searching for treasure."
    }
  ]
}
```
- Manga Details
```
GET /manga/{mangaId}
```
Response Example:
```json
{
  "mangaId": 1,
  "title": "One Piece",
  "originalLanguage": "ja",
  "publicationStatus": "ongoing",
  "releaseYear": 1997,
  "description": "A story about pirates searching for treasure.",
  "images": [
    {"imageId": 101, "url": "https://cdn.example.com/onepiece-cover.jpg", "type": "cover"}
  ],
  "authors": [{"authorId": 1, "name": "Eiichiro Oda"}],
  "genres": ["Action", "Adventure"],
  "tags": ["Pirates", "Comedy"],
  "latestChapters": [
    {"chapterId": 501, "title": "Chapter 1050", "chapterNumber": 1050.0}
  ]
}
```
- List Chapters
```
GET /manga/{mangaId}/chapters
```
Response Example:
```json
[
  {
    "chapterId": 501,
    "volumeNumber": 105,
    "chapterNumber": 1050.0,
    "canonicalTitle": "The Final Battle",
    "uploads": [
      {
        "uploadId": 1001,
        "groupName": "Scanlation Team",
        "language": "en",
        "publishedAt": "2025-08-27T12:00:00Z"
      }
    ]
  }
]
```

## Chapters & Pages
- Chapter Upload pages
```
GET /uploads/{uploadId}/pages
```
Response Example:
```json
{
  "uploadId": 1001,
  "chapterId": 501,
  "pages": [
    {"pageNumber": 1, "imageUrl": "https://cdn.example.com/onepiece/1050/1.jpg"}
  ]
}
```

## Users
- Get User Profile
```
GET /users/{userId}
```
Response Example:
```json
{
  "userId": 42,
  "username": "mangaFan123",
  "role": "user",
  "joinedAt": "2024-05-01T15:00:00Z",
  "uploadedChapters": 10,
  "reviewsCount": 15
}
```

## Ratings & Reviews
- Manga Ratings
```
GET /manga/{mangaId}/ratings
```
Response Example:
```json
{
  "mangaId": 1,
  "averageRating": 9.2,
  "ratingsCount": 1200,
  "distribution": {"1":2,"2":0,"3":1,"4":2,"5":5,"6":10,"7":50,"8":200,"9":400,"10":530}
}
```
- Manga Reviews
```
GET /manga/{mangaId}/reviews?page=1&limit=10
```
Response Example:
```json
[
  {
    "userId": 42,
    "username": "mangaFan123",
    "rating": 10,
    "review": "Amazing story and art!",
    "createdAt": "2025-08-01T10:00:00Z"
  }
]
```

# Error Handling
404 Not Found: {"error": "Manga not found"}
400 Bad Request: {"error": "Invalid query parameter: year"}
401 Unauthorized (private endpoints): {"error": "Missing or invalid token"}
500 Internal Server Error: {"error": "Unexpected server error"}