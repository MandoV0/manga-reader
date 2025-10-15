'use client';

import { useState, useEffect } from 'react';
import { submitRating, isAuthenticated, getUserRating } from '@/lib/api';
import Link from 'next/link';
import { Star, MessageSquare, Send, Edit3 } from 'lucide-react';

/**
 * MangaRating component for users to create a rating for a manga.
 * Changes depending on if we already have a rating or not.
 */
export default function MangaRating({ seriesId }: { seriesId: number }) {
  const [rating, setRating] = useState(0);
  const [comment, setComment] = useState('');
  const [submitted, setSubmitted] = useState(false);
  const [hasLoaded, setHasLoaded] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [isAuth, setIsAuth] = useState(false);
  const [loading, setLoading] = useState(false);
  const [existingRating, setExistingRating] = useState<number | null>(null);
  const [isEditing, setIsEditing] = useState(false);
  const [initialLoading, setInitialLoading] = useState(true);

  useEffect(() => {
    const loadData = async () => {
      const auth = isAuthenticated();
      setIsAuth(auth);
      
      if (auth) {
        try {
          const userRating = await getUserRating(seriesId);
          if (userRating) {
            setExistingRating(userRating.stars);
            setRating(userRating.stars);
          }
        } catch (error) {
          console.error("Failed to load existing rating:", error);
        }
      }
      setInitialLoading(false);
      setHasLoaded(true);
    };
    
    loadData();
  }, [seriesId]);

  const handleSubmit = async () => {
    if (rating === 0) {
      setError('Please select a rating');
      return;
    }

    setLoading(true);
    setError(null);
    
    try {
      await submitRating(seriesId, rating, comment);
      setSubmitted(true);
      setExistingRating(rating);
      setIsEditing(false);
    } catch (err: unknown) {
      if (err instanceof Error) {
        setError(err.message);
      } else {
        setError("An unknown error occurred");
      }
    } finally {
      setLoading(false);
    }
  };

  const handleEdit = () => {
    setIsEditing(true);
    setSubmitted(false);
  };

  if (initialLoading) {
    return (
      <div className="text-center py-6">
        <div className="animate-spin rounded-full h-8 w-8 border-b-2 border-red-500 mx-auto mb-4"></div>
        <p className="text-gray-400 text-sm">Loading rating...</p>
      </div>
    );
  }

  if (!isAuth) {
    return (
      <div className="text-center py-6">
        <Star className="h-8 w-8 text-gray-500 mx-auto mb-2" />
        <p className="text-gray-400 text-sm mb-3">Sign in to rate this manga</p>
        <Link 
          href="/login" 
          className="netflix-gradient text-white px-4 py-2 rounded-lg text-sm font-medium hover:opacity-90 transition-opacity"
        >
          Sign In
        </Link>
      </div>
    );
  }

  if (existingRating && !isEditing) {
    return (
      <div>
        <div className="flex items-center justify-between mb-4">
          <div className="flex items-center space-x-2">
            <Star className="h-5 w-5 text-yellow-400 fill-current" />
            <h3 className="text-lg font-semibold text-white">Your Rating</h3>
          </div>
          <button
            onClick={handleEdit}
            className="flex items-center space-x-1 text-gray-400 hover:text-white transition-colors"
          >
            <Edit3 className="h-4 w-4" />
            <span className="text-sm">Edit</span>
          </button>
        </div>

        <div className="flex items-center space-x-4 mb-4">
          <div className="flex gap-1">
            {Array.from({ length: 10 }, (_, i) => (
              <Star
                key={i}
                className={`h-6 w-6 ${
                  i < existingRating ? 'text-yellow-400 fill-current' : 'text-gray-500'
                }`}
              />
            ))}
          </div>
          <span className="text-white font-medium">{existingRating}/10</span>
        </div>

        {submitted && (
          <div className="text-center py-4">
            <div className="inline-flex items-center space-x-2 text-green-400 bg-green-500/20 px-4 py-2 rounded-lg">
              <Star className="h-4 w-4 fill-current" />
              <span className="font-medium">Rating updated! Thank you.</span>
            </div>
          </div>
        )}
      </div>
    );
  }

  return (
    <div>
      <div className="flex items-center space-x-2 mb-4">
        <Star className="h-5 w-5 text-yellow-400" />
        <h3 className="text-lg font-semibold text-white">Rate this Manga</h3>
      </div>

      <div className="mb-4">
        <div className="flex gap-1 mb-2">
          {Array.from({ length: 10 }, (_, i) => (
            <button
              key={i}
              className={`w-8 h-8 transition-colors ${
                i < rating 
                  ? 'text-yellow-400' 
                  : 'text-gray-500 hover:text-yellow-300'
              }`}
              onClick={() => setRating(i + 1)}
            >
              <Star className={`w-full h-full ${i < rating ? 'fill-current' : ''}`} />
            </button>
          ))}
        </div>
        <p className="text-sm text-gray-400">
          {rating > 0 ? `${rating}/10` : 'Select a rating'}
        </p>
      </div>

      <div className="mb-4">
        <div className="flex items-center space-x-2 mb-2">
          <MessageSquare className="h-4 w-4 text-gray-400" />
          <label className="text-sm font-medium text-gray-300">Comment (Optional)</label>
        </div>
        <textarea
          className="w-full p-3 rounded-lg bg-gray-700 border border-gray-600 text-white placeholder-gray-400 focus:border-red-500 focus:outline-none resize-none"
          placeholder="Share your thoughts about this manga..."
          value={comment}
          onChange={(e) => setComment(e.target.value)}
          rows={3}
          maxLength={500}
        />
        <p className="text-xs text-gray-500 mt-1">{comment.length}/500</p>
      </div>

      <button
        className="w-full flex items-center justify-center space-x-2 netflix-gradient text-white py-3 rounded-lg font-medium hover:opacity-90 transition-opacity disabled:opacity-50"
        onClick={handleSubmit}
        disabled={loading || rating === 0}
      >
        {loading ? (
          <div className="animate-spin rounded-full h-4 w-4 border-b-2 border-white"></div>
        ) : (
          <>
            <Send className="h-4 w-4" />
            <span>Submit Rating</span>
          </>
        )}
      </button>

      {error && (
        <div className="mt-3 p-3 bg-red-500/20 border border-red-500/50 rounded-lg">
          <p className="text-red-400 text-sm">{error}</p>
        </div>
      )}
    </div>
  );
}
