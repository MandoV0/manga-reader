export const API_URL = "http://localhost:5110/api/v1";

export interface LoginRequestDto {
  email?: string;
  password?: string;
}

export interface RegisterRequestDto {
  username?: string;
  email?: string;
  password?: string;
}

export interface TokenDto {
  token?: string;
}

export interface SeriesListDto {
  id: number;
  title: string;
  coverImageUrl: string;
  bannerImageUrl: string;
}

export interface SeriesDto {
  id: number;
  title: string;
  description: string;
  coverImageUrl: string;
  bannerImageUrl: string;
  author: string;
  publisher: string;
  status: string;
  genres: string[];
  releaseDate: string;
  averageRating: number;
}

export interface ChapterDto {
  id: number;
  title: string;
  pages: PageDto[];
  pagesCount: number;
  viewsCount: number;
}

export interface PageDto {
  id: number;
  pageNumber: number;
  imageUrl: string;
}

export interface Genre {
  id: number;
  name: string;
}

export interface PagedResponseDto<T> {
  items: T[];
  page: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
}

export interface CreateRatingDto {
  rating: number;
  comment?: string;
}

export interface LastReadChapterDto {
  seriesId: number;
  chapterId: number;
  seriesTitle: string;
  chapterTitle: string;
  coverImageUrl: string;
}

export interface UpdateLastReadChapterDto {
  seriesId: number;
  lastReadChapterId: number;
}

export interface ChangePasswordRequest {
  currentPassword?: string;
  newPassword?: string;
}

/**
 * Logs in a user.
 * @param data The login credentials.
 * @returns A promise that resolves to a token DTO.
 */
export async function login(data: LoginRequestDto): Promise<TokenDto> {
  const res = await fetch(`${API_URL}/Auth/login`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(data),
  });

  if (!res.ok) {
    const errorText = await res.text();
    throw new Error(errorText || "Failed to login");
  }

  return res.json();
}

/**
 * Registers a new user.
 * @param data The registration data.
 * @returns A promise that resolves to a token DTO.
 */
export async function register(data: RegisterRequestDto): Promise<TokenDto> {
  const res = await fetch(`${API_URL}/Auth/register`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(data),
  });

  if (!res.ok) {
    const errorText = await res.text();
    throw new Error(errorText || "Failed to register");
  }

  return res.json();
}

/**
 * Sends a password reset email.
 * @param email The user's email address.
 */
export async function forgotPassword(email: string): Promise<void> {
  const res = await fetch(`${API_URL}/Auth/forgot-password?email=${encodeURIComponent(email)}`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
  });

  if (!res.ok) {
    throw new Error("Failed to send reset email");
  }
}

/**
 * Resets the user's password.
 * @param token The password reset token.
 * @param email The user's email address.
 * @param newPassword The new password.
 */
export async function resetPassword(token: string, email: string, newPassword: string): Promise<void> {
  const res = await fetch(`${API_URL}/Auth/reset-password?token=${encodeURIComponent(token)}&email=${encodeURIComponent(email)}`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(newPassword),
  });

  if (!res.ok) {
    throw new Error("Failed to reset password");
  }
}

/**
 * Changes the user's password.
 * @param data The current and new password.
 */
export async function changePassword(data: ChangePasswordRequest): Promise<void> {
  const res = await fetch(`${API_URL}/Auth/change-password`, {
    method: "POST",
    headers: getAuthHeaders(),
    body: JSON.stringify(data),
  });

  if (!res.ok) {
    throw new Error("Failed to change password");
  }
}

/**
 * Checks if the user is authenticated.
 * @returns True if the user is authenticated, false otherwise.
 */
export function isAuthenticated(): boolean {
  if (typeof window === 'undefined') return false;
  const token = localStorage.getItem("token");
  return !!token;
}

/**
 * Gets the authentication headers for API requests.
 * @returns The authentication headers.
 */
export function getAuthHeaders(): HeadersInit {
  const token = typeof window !== 'undefined' ? localStorage.getItem("token") : null;
  return {
    "Content-Type": "application/json",
    ...(token && { Authorization: `Bearer ${token}` }),
  };
}

/**
 * Fetches a list of series from the specified endpoint.
 * @param endpoint The API endpoint to fetch the series from.
 * @returns A promise that resolves to an array of series.
 */
export async function fetchSeries(endpoint: string): Promise<SeriesListDto[]> {
  const res = await fetch(`${API_URL}/Series/${endpoint}`);
  if (!res.ok) throw new Error("Failed to fetch series");
  return res.json();
}

/**
 * Fetches a paginated list of all series.
 * @param page The page number to fetch.
 * @param pageSize The number of series per page.
 * @param sort The sorting criteria.
 * @returns A promise that resolves to a paginated response of series.
 */
export async function fetchAllSeries(page = 1, pageSize = 20, sort = ""): Promise<PagedResponseDto<SeriesListDto>> {
  const params = new URLSearchParams({
    page: page.toString(),
    pageSize: pageSize.toString(),
    ...(sort && { sort }),
  });
  
  const res = await fetch(`${API_URL}/Series?${params}`);
  if (!res.ok) throw new Error("Failed to fetch series");
  return res.json();
}

/**
 * Fetches a single series by its ID.
 * @param id The ID of the series to fetch.
 * @returns A promise that resolves to the series data.
 */
export async function fetchSeriesById(id: number): Promise<SeriesDto> {
  const res = await fetch(`${API_URL}/Series/${id}`);
  if (!res.ok) throw new Error("Failed to fetch series");
  return res.json();
}

/**
 * Fetches the chapters for a given series.
 * @param seriesId The ID of the series.
 * @returns A promise that resolves to an array of chapters.
 */
export async function fetchChapters(seriesId: number): Promise<ChapterDto[]> {
  const res = await fetch(`${API_URL}/Series/${seriesId}/chapters`);
  if (!res.ok) throw new Error("Failed to fetch chapters");
  return res.json();
}

/**
 * Fetches a single chapter by its ID.
 * @param chapterId The ID of the chapter to fetch.
 * @returns A promise that resolves to the chapter data.
 */
export async function fetchChapterById(chapterId: number): Promise<ChapterDto> {
  const res = await fetch(`${API_URL}/Series/chapters/${chapterId}`);
  if (!res.ok) throw new Error("Failed to fetch chapter");
  return res.json();
}

/**
 * Fetches a list of all genres.
 * @returns A promise that resolves to an array of genres.
 */
export async function fetchGenres(): Promise<Genre[]> {
  const res = await fetch(`${API_URL}/Genres`);
  if (!res.ok) throw new Error("Failed to fetch genres");
  return res.json();
}

/**
 * Submits a rating for a series.
 * @param seriesId The ID of the series to rate.
 * @param rating The rating to submit.
 * @param comment An optional comment.
 */
export async function submitRating(seriesId: number, rating: number, comment?: string): Promise<void> {
  const res = await fetch(`${API_URL}/series/${seriesId}/rate`, {
    method: "POST",
    headers: getAuthHeaders(),
    body: JSON.stringify({ rating, comment }),
  });

  if (!res.ok) {
    throw new Error("Failed to submit rating");
  }
}

/**
 * Updates a rating for a series.
 * @param seriesId The ID of the series to update the rating for.
 * @param rating The new rating.
 * @param comment An optional comment.
 */
export async function updateRating(seriesId: number, rating: number, comment?: string): Promise<void> {
  const res = await fetch(`${API_URL}/series/${seriesId}/rate`, {
    method: "PUT",
    headers: getAuthHeaders(),
    body: JSON.stringify({ rating, comment }),
  });

  if (!res.ok) {
    throw new Error("Failed to update rating");
  }
}

/**
 * Deletes a rating for a series.
 * @param seriesId The ID of the series to delete the rating for.
 */
export async function deleteRating(seriesId: number): Promise<void> {
  const res = await fetch(`${API_URL}/series/${seriesId}/rate`, {
    method: "DELETE",
    headers: getAuthHeaders(),
  });

  if (!res.ok) {
    throw new Error("Failed to delete rating");
  }
}

/**
 * Updates the last read chapter for a series.
 * @param seriesId The ID of the series.
 * @param lastReadChapterId The ID of the last read chapter.
 */
export async function updateLastReadChapter(seriesId: number, lastReadChapterId: number): Promise<void> {
  const res = await fetch(`${API_URL}/Series/last-read`, {
    method: "POST",
    headers: getAuthHeaders(),
    body: JSON.stringify({ seriesId, lastReadChapterId }),
  });

  if (!res.ok) {
    throw new Error("Failed to update last read chapter");
  }
}

/**
 * Gets the last read chapters for the current user.
 * @param limit The maximum number of chapters to return.
 * @param offset The number of chapters to skip.
 * @returns A promise that resolves to an array of last read chapters.
 */
export async function getLastReadChapters(limit = 10, offset = 0): Promise<LastReadChapterDto[]> {
  const params = new URLSearchParams({
    limit: limit.toString(),
    offset: offset.toString(),
  });
  
  const res = await fetch(`${API_URL}/Series/last-read?${params}`, {
    headers: getAuthHeaders(),
  });
  
  if (!res.ok) throw new Error("Failed to fetch last read chapters");
  return res.json();
}

/**
 * Searches for series based on a query.
 * @param query The search query.
 * @param page The page number to fetch.
 * @param pageSize The number of results per page.
 * @returns A promise that resolves to a paginated response of series.
 */
export async function searchSeries(query: string, page = 1, pageSize = 20): Promise<PagedResponseDto<SeriesListDto>> {
  const params = new URLSearchParams({
    query,
    page: page.toString(),
    pageSize: pageSize.toString(),
  });
  
  const res = await fetch(`${API_URL}/Series?${params}`);
  if (!res.ok) throw new Error("Failed to search series");
  return res.json();
}

/**
 * Tracks a view for a manga series.
 * @param seriesId The ID of the series to track.
 */
export async function trackMangaView(seriesId: number): Promise<void> {
  if (!isAuthenticated()) return;
  
  try {
    const token = typeof window !== 'undefined' ? localStorage.getItem("token") : null;
    await fetch(`${API_URL}/Series/${seriesId}/view`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        ...(token && { Authorization: `Bearer ${token}` }),
      },
    });
  } catch (error) {
    console.error("Failed to track manga view:", error);
  }
}

/**
 * Gets the last read chapter for a specific series.
 * @param seriesId The ID of the series.
 * @returns A promise that resolves to the last read chapter or null if not found.
 */
export async function getLastReadChapterForSeries(seriesId: number): Promise<LastReadChapterDto | null> {
  if (!isAuthenticated()) return null;
  
  try {
    const chapters = await getLastReadChapters(100, 0);
    return chapters.find(chapter => chapter.seriesId === seriesId) || null;
  } catch (error) {
    console.error("Failed to get last read chapter:", error);
    return null;
  }
}

/**
 * Gets the user's rating for a specific series.
 * @param seriesId The ID of the series.
 * @returns A promise that resolves to the user's rating or null if not found.
 */
export async function getUserRating(seriesId: number): Promise<{ stars: number } | null> {
  if (!isAuthenticated()) return null;
  
  try {
    const res = await fetch(`${API_URL}/Series/${seriesId}/rate`, {
      headers: getAuthHeaders(),
    });
    
    if (res.status === 404) return null;
    if (!res.ok) {
      console.error(`Failed to fetch user rating: ${res.status} ${res.statusText}`);
      return null;
    }
    
    const data = await res.json();
    return { stars: data.stars };
  } catch (error) {
    console.error("Failed to get user rating:", error);
    return null;
  }
}

/**
 * Clears the user's reading history.
 */
export async function clearReadingHistory(): Promise<void> {
  if (!isAuthenticated()) return;
  
  try {
    const res = await fetch(`${API_URL}/Series/reading-history`, {
      method: "DELETE",
      headers: getAuthHeaders(),
    });
    
    if (!res.ok) {
      console.error(`Failed to clear reading history: ${res.status} ${res.statusText}`);
      throw new Error("Failed to clear reading history");
    }
  } catch (error) {
    console.error("Failed to clear reading history:", error);
    throw error;
  }
}

/**
 * Deletes the user's account.
 */
export async function deleteAccount(): Promise<void> {
  const res = await fetch(`${API_URL}/Auth/user`, {
    method: "DELETE",
    headers: getAuthHeaders(),
  });

  if (!res.ok) {
    throw new Error("Failed to delete account");
  }
}
