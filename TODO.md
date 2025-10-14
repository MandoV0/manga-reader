## TODO

- **Core auth** [Done completely]
  - Implement JWT auth: signup/login, password hashing, token issuance, middleware
  - Protect endpoints with `[Authorize]` and configure JWT bearer in startup
  - Configure Swagger with JWT security scheme

- **Users** [On Hold as i want to try OAuth out later.]
  - Add `User` entity/DbSet
  - Endpoints: GET/PUT `user/profile`, DELETE `user/account`
  - DTOs: `UserProfileDto`

- **Saved series**
  - Add user–series join entity
  - Endpoints: GET/POST/DELETE `user/saved-series` with pagination

- **Reading history**
  - Add `ReadingHistory` entity
  - Endpoints: GET/POST `user/history` with pagination

- **Ratings** [Almost done Just need to recalculate the average rating of a Series after a User Creates/Updates/Deletes a review]
  - Add ratings for series and chapters
  - Endpoints: POST `/series/{seriesId}/rate`, `/chapters/{chapterId}/rate`
  - Update and persist average ratings

- **Reviews** [Scrapped, just doing ratings and could easily add optional comments later. ]]
  - Add `Review` entity
  - Endpoints: list for series, create, update, delete
  - DTOs: `ReviewDto`
 
- **Chapters API**
  - Endpoints: GET `/chapters/{chapterId}`, GET `/chapters/{chapterId}/pages` with pagination

- **Search**
  - Implement `/search` with query, include/exclude genres, status, sort, pagination

- **Genres**
  - Implement `/genres` list

- **Series**
  - Enhance GET `/series` to honor sort options per spec
  - Return pagination metadata (e.g., `PaginatedResponse<T>`)

- **DTOs and responses**
  - Add `PaginatedResponse<T>`, rating request DTOs

- **Data layer**
  - Extend EF model with new entities/relationships
  - Create and run migrations
  - Update `seed.sql` for new data

- **Quality**
  - Add validation (DataAnnotations/FluentValidation) and error handling middleware
  - Optional: caching for trending/popular/recent
  - Optional: unit tests for new services/repositories