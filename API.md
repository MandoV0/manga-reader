# API Schema for Manga Reader

This document outlines the API structure for the Manga Reader API.

## Authentication

### POST /auth/signup
- **Description:** Creates a new user account.
- **Request Body:**
  ```json
  {
    "username": "string",
    "email": "string",
    "password": "string"
  }
  ```
- **Response:**
  ```json
  {
    "userId": "string",
    "username": "string",
    "email": "string"
  }
  ```

### POST /auth/login
- **Description:** Authenticates a user and returns a JWT token.
- **Request Body:**
  ```json
  {
    "email": "string",
    "password": "string"
  }
  ```
- **Response:**
  ```json
  {
    "token": "string"
  }
  ```

## Series

### GET /series
- **Description:** Gets a paginated list of series.
- **Query Parameters:**
  - `page` (int, default: 1): The page number to retrieve.
  - `pageSize` (int, default: 20): The number of series per page.
  - `sort` (string, default: "popular"): Sorting criteria. Possible values: `popular`, `trending`, `recently-updated`, `rating`.
- **Response:** A paginated list of `SeriesListDto`.

### GET /series/{seriesId}
- **Description:** Gets detailed information for a specific series.
- **Response:** `SeriesDto`.

### GET /series/{seriesId}/chapters
- **Description:** Gets a paginated list of chapters for a series.
- **Query Parameters:**
  - `page` (int, default: 1): The page number to retrieve.
  - `pageSize` (int, default: 50): The number of chapters per page.
- **Response:** A paginated list of `ChapterDto`.

## Chapters

### GET /chapters/{chapterId}
- **Description:** Gets details for a specific chapter.
- **Response:** `ChapterDto`.

### GET /chapters/{chapterId}/pages
- **Description:** Gets a paginated list of pages for a chapter, allowing for optimized reading.
- **Query Parameters:**
  - `page` (int, default: 1): The page number to retrieve.
  - `pageSize` (int, default: 1): The number of pages to load at a time.
- **Response:**
  ```json
  {
    "chapterId": "integer",
    "totalPages": "integer",
    "currentPage": "integer",
    "pages": [
      {
        "pageNumber": "integer",
        "url": "string"
      }
    ]
  }
  ```

## User

(Requires authentication via JWT token in the `Authorization` header)

### GET /user/profile
- **Description:** Gets the profile of the currently authenticated user.
- **Response:**
  ```json
  {
    "username": "string",
    "email": "string",
    "joinDate": "date"
  }
  ```

### PUT /user/profile
- **Description:** Updates the profile of the currently authenticated user.
- **Request Body:**
  ```json
  {
    "username": "string",
    "email": "string"
  }
  ```
- **Response:**
  ```json
  {
    "username": "string",
    "email": "string",
    "joinDate": "date"
  }
  ```

### DELETE /user/account
- **Description:** Deletes the account of the currently authenticated user.
- **Response:** `204 No Content`

### GET /user/saved-series
- **Description:** Gets a paginated list of series saved by the user.
- **Query Parameters:**
  - `page` (int, default: 1): The page number to retrieve.
  - `pageSize` (int, default: 20): The number of series per page.
- **Response:** A paginated list of `SeriesListDto`.

### POST /user/saved-series
- **Description:** Adds a series to the user's saved list.
- **Request Body:**
  ```json
  {
    "seriesId": "integer"
  }
  ```
- **Response:** `201 Created`

### DELETE /user/saved-series/{seriesId}
- **Description:** Removes a series from the user's saved list.
- **Response:** `204 No Content`

### GET /user/history
- **Description:** Gets the user's reading history.
- **Query Parameters:**
  - `page` (int, default: 1): The page number to retrieve.
  - `pageSize` (int, default: 20): The number of items per page.
- **Response:** A paginated list of history entries.
  ```json
  [
    {
      "seriesId": "integer",
      "chapterId": "integer",
      "lastReadPage": "integer",
      "lastReadDate": "date"
    }
  ]
  ```

### POST /user/history
- **Description:** Adds or updates an entry in the user's reading history.
- **Request Body:**
  ```json
  {
    "chapterId": "integer",
    "pageNumber": "integer"
  }
  ```
- **Response:** `200 OK`

## Ratings

(Requires authentication)

### POST /series/{seriesId}/rate
- **Description:** Submits a rating for a series.
- **Request Body:**
  ```json
  {
    "rating": "integer" //  1-5
  }
  ```
- **Response:** `201 Created`

### POST /chapters/{chapterId}/rate
- **Description:** Submits a rating for a chapter.
- **Request Body:**
  ```json
  {
    "rating": "integer" //  1-5
  }
  ```
- **Response:** `201 Created`

## Reviews

(Requires authentication)

### GET /series/{seriesId}/reviews
- **Description:** Gets all reviews for a specific series.
- **Response:**
  ```json
  [
    {
      "id": "integer",
      "userId": "string",
      "username": "string",
      "rating": "integer",
      "reviewText": "string",
      "createdAt": "date"
    }
  ]
  ```

### POST /series/{seriesId}/reviews
- **Description:** Adds a review for a series.
- **Request Body:**
  ```json
  {
    "rating": "integer", // 1-5
    "reviewText": "string"
  }
  ```
- **Response:** `201 Created`

### PUT /reviews/{reviewId}
- **Description:** Updates an existing review.
- **Request Body:**
  ```json
  {
    "rating": "integer", // 1-5
    "reviewText": "string"
  }
  ```
- **Response:** `200 OK`

### DELETE /reviews/{reviewId}
- **Description:** Deletes a review.
- **Response:** `204 No Content`

## Search

### GET /search
- **Description:** Searches for series based on multiple criteria.
- **Query Parameters:**
  - `query` (string): Text to search in title, description, and author.
  - `includeGenres` (string): Comma-separated list of genre names to include.
  - `excludeGenres` (string): Comma-separated list of genre names to exclude.
  - `status` (string): `ongoing`, `finished`, `canceled`.
  - `sort` (string): `rating`, `views`, `recentlyUpdated`, `releaseDate`.
  - `page` (int, default: 1): The page number to retrieve.
- **Response:** A paginated list of `SeriesListDto`.

## Genres

### GET /genres
- **Description:** Gets a list of all available genres.
- **Response:**
  ```json
  [
    {
      "id": "integer",
      "name": "string"
    }
  ]
  ```
