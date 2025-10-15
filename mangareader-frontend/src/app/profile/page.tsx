"use client";
import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import { isAuthenticated, getLastReadChapters, clearReadingHistory, deleteAccount, changePassword } from "@/lib/api";
import Navbar from "@/components/Navbar";
import MangaCard from "@/components/MangaCard";
import { User, Settings, LogOut, Trash2, BookOpen, History, AlertTriangle, Lock, Eye, EyeOff } from "lucide-react";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import toast from "react-hot-toast";

/**
 * Profile page for users to view their profile, reading history, and manage account settings.
 */
export default function ProfilePage() {
  const router = useRouter();
  const [isAuth, setIsAuth] = useState(false);
  const [lastReadChapters, setLastReadChapters] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);
  const [showDeleteConfirm, setShowDeleteConfirm] = useState(false);
  const [showClearHistoryConfirm, setShowClearHistoryConfirm] = useState(false);
  const [username, setUsername] = useState<string>("");

  const [currentPassword, setCurrentPassword] = useState("");
  const [newPassword, setNewPassword] = useState("");
  const [confirmNewPassword, setConfirmNewPassword] = useState("");
  const [showChangePasswordForm, setShowChangePasswordForm] = useState(false);
  const [changePasswordLoading, setChangePasswordLoading] = useState(false);
  const [showCurrentPassword, setShowCurrentPassword] = useState(false);
  const [showNewPassword, setShowNewPassword] = useState(false);
  const [showConfirmNewPassword, setShowConfirmNewPassword] = useState(false);

  useEffect(() => {
    const auth = isAuthenticated();
    setIsAuth(auth);
    if (!auth) {
      router.push("/login");
      return;
    }

    loadLastReadChapters();
    loadUsername();
  }, [router]);

  const loadLastReadChapters = async () => {
    try {
      const chapters = await getLastReadChapters(10);
      setLastReadChapters(chapters);
    } catch (error) {
      console.error("Failed to load last read chapters:", error);
    } finally {
      setLoading(false);
    }
  };

  const loadUsername = () => {
    try {
      const token = localStorage.getItem("token");
      if (token) {
        const payload = JSON.parse(atob(token.split('.')[1]));
        console.log("JWT Payload:", payload);
        setUsername(payload.username || payload.unique_name || payload.sub || payload.name || "User");
      }
    } catch (error) {
      console.error("Failed to load username:", error);
      setUsername("User");
    }
  };

  const handleClearHistory = async () => {
    try {
      await clearReadingHistory();
      setLastReadChapters([]);
      setShowClearHistoryConfirm(false);
      toast.success("Reading history cleared!");
    } catch (error) {
      console.error("Failed to clear reading history:", error);
      toast.error("Failed to clear reading history.");
    }
  };

  const handleLogout = () => {
    localStorage.removeItem("token");
    setIsAuth(false);
    router.push("/");
    toast.success("Logged out successfully!");
  };

  const handleDeleteAccount = async () => {
    if (showDeleteConfirm) {
      try {
        await deleteAccount();
        localStorage.removeItem("token");
        setIsAuth(false);
        router.push("/");
        toast.success("Account deleted successfully!");
      } catch (error) {
        console.error("Failed to delete account:", error);
        toast.error("Failed to delete account. Please try again.");
      }
      setShowDeleteConfirm(false);
    } else {
      setShowDeleteConfirm(true);
    }
  };

  const handleChangePassword = async (e: React.FormEvent) => {
    e.preventDefault();
    setChangePasswordLoading(true);

    if (newPassword !== confirmNewPassword) {
      toast.error("New passwords do not match.");
      setChangePasswordLoading(false);
      return;
    }

    if (newPassword.length < 8) {
      toast.error("New password must be at least 8 characters long.");
      setChangePasswordLoading(false);
      return;
    }

    try {
      await changePassword({ currentPassword, newPassword });
      toast.success("Password changed successfully!");
      setCurrentPassword("");
      setNewPassword("");
      setConfirmNewPassword("");
      setShowChangePasswordForm(false);
    } catch (error: any) {
      toast.error(error.message || "Failed to change password.");
    } finally {
      setChangePasswordLoading(false);
    }
  };

  if (!isAuth) {
    return null;
  }

  return (
    <div className="min-h-screen">
      <Navbar />
      
      <div className="pt-20 pb-8">
        <div className="container mx-auto px-6">
          <div className="flex items-center space-x-4 mb-8">
            <div className="netflix-gradient p-4 rounded-full">
              <User className="h-8 w-8 text-white" />
            </div>
            <div>
              <h1 className="text-3xl font-bold text-white">Welcome, {username}</h1>
              <p className="text-gray-400">Manage your account and reading history</p>
            </div>
          </div>

          <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
            <div className="lg:col-span-2 space-y-8">
              <div className="bg-gray-800/50 rounded-lg p-6">
                <div className="flex items-center justify-between mb-4">
                  <div className="flex items-center space-x-2">
                    <History className="h-5 w-5 text-red-500" />
                    <h2 className="text-xl font-semibold text-white">Continue Reading</h2>
                  </div>
                  {lastReadChapters.length > 0 && (
                    <button
                      onClick={() => setShowClearHistoryConfirm(true)}
                      className="flex items-center space-x-1 text-gray-400 hover:text-red-400 transition-colors text-sm"
                    >
                      <Trash2 className="h-4 w-4" />
                      <span>Clear History</span>
                    </button>
                  )}
                </div>
                
                {loading ? (
                  <div className="text-center py-8">
                    <div className="animate-spin rounded-full h-8 w-8 border-b-2 border-red-500 mx-auto"></div>
                    <p className="text-gray-400 mt-2">Loading...</p>
                  </div>
                ) : lastReadChapters.length > 0 ? (
                  <div className="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-4">
                    {lastReadChapters.map((chapter) => (
                      <div key={chapter.seriesId} className="cursor-pointer">
                        <MangaCard
                          id={chapter.seriesId}
                          title={chapter.seriesTitle}
                          coverUrl={chapter.coverImageUrl}
                        />
                        <p className="text-sm text-gray-400 mt-2 truncate">
                          {chapter.chapterTitle}
                        </p>
                      </div>
                    ))}
                  </div>
                ) : (
                  <div className="text-center py-8">
                    <BookOpen className="h-12 w-12 text-gray-500 mx-auto mb-4" />
                    <p className="text-gray-400">No reading history yet</p>
                    <p className="text-gray-500 text-sm">Start reading manga to see your history here</p>
                  </div>
                )}
              </div>
            </div>

            <div className="space-y-6">
              <div className="bg-gray-800/50 rounded-lg p-6">
                <div className="flex items-center space-x-2 mb-4">
                  <Settings className="h-5 w-5 text-red-500" />
                  <h2 className="text-xl font-semibold text-white">Account Settings</h2>
                </div>
                
                <div className="space-y-4">
                  <button
                    onClick={handleLogout}
                    className="w-full flex items-center space-x-3 p-3 rounded-lg bg-gray-700 hover:bg-gray-600 transition-colors text-white"
                  >
                    <LogOut className="h-5 w-5" />
                    <span>Sign Out</span>
                  </button>

                  <Button
                    onClick={() => setShowChangePasswordForm(!showChangePasswordForm)}
                    className="w-full flex items-center space-x-3 p-3 rounded-lg bg-gray-700 hover:bg-gray-600 transition-colors text-white"
                  >
                    <Lock className="h-5 w-5" />
                    <span>{showChangePasswordForm ? "Cancel Change Password" : "Change Password"}</span>
                  </Button>

                  {showChangePasswordForm && (
                    <form onSubmit={handleChangePassword} className="space-y-4 p-4 bg-gray-700/50 rounded-lg">
                      <h3 className="text-lg font-semibold text-white">Change Password</h3>
                      
                      <div>
                        <label className="block text-sm font-medium text-gray-300 mb-2">Current Password</label>
                        <div className="relative">
                          <Input
                            type={showCurrentPassword ? "text" : "password"}
                            value={currentPassword}
                            onChange={(e) => setCurrentPassword(e.target.value)}
                            className="w-full pl-3 pr-10 bg-gray-800 border-gray-600 text-white placeholder-gray-400 focus:border-red-500"
                            required
                          />
                          <button
                            type="button"
                            onClick={() => setShowCurrentPassword(!showCurrentPassword)}
                            className="absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-400 hover:text-white"
                          >
                            {showCurrentPassword ? <EyeOff className="h-5 w-5" /> : <Eye className="h-5 w-5" />}
                          </button>
                        </div>
                      </div>
                      <div>
                        <label className="block text-sm font-medium text-gray-300 mb-2">New Password</label>
                        <div className="relative">
                          <Input
                            type={showNewPassword ? "text" : "password"}
                            value={newPassword}
                            onChange={(e) => setNewPassword(e.target.value)}
                            className="w-full pl-3 pr-10 bg-gray-800 border-gray-600 text-white placeholder-gray-400 focus:border-red-500"
                            required
                            minLength={8}
                          />
                          <button
                            type="button"
                            onClick={() => setShowNewPassword(!showNewPassword)}
                            className="absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-400 hover:text-white"
                          >
                            {showNewPassword ? <EyeOff className="h-5 w-5" /> : <Eye className="h-5 w-5" />}
                          </button>
                        </div>
                      </div>
                      <div>
                        <label className="block text-sm font-medium text-gray-300 mb-2">Confirm New Password</label>
                        <div className="relative">
                          <Input
                            type={showConfirmNewPassword ? "text" : "password"}
                            value={confirmNewPassword}
                            onChange={(e) => setConfirmNewPassword(e.target.value)}
                            className="w-full pl-3 pr-10 bg-gray-800 border-gray-600 text-white placeholder-gray-400 focus:border-red-500"
                            required
                          />
                          <button
                            type="button"
                            onClick={() => setShowConfirmNewPassword(!showConfirmNewPassword)}
                            className="absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-400 hover:text-white"
                          >
                            {showConfirmNewPassword ? <EyeOff className="h-5 w-5" /> : <Eye className="h-5 w-5" />}
                          </button>
                        </div>
                      </div>
                      <Button
                        type="submit"
                        disabled={changePasswordLoading}
                        className="w-full netflix-gradient text-white py-2 font-semibold hover:opacity-90 disabled:opacity-50"
                      >
                        {changePasswordLoading ? "Changing Password..." : "Save New Password"}
                      </Button>
                    </form>
                  )}
                  
                  <button
                    onClick={handleDeleteAccount}
                    className={`w-full flex items-center space-x-3 p-3 rounded-lg transition-colors ${
                      showDeleteConfirm 
                        ? 'bg-red-600 hover:bg-red-700 text-white' 
                        : 'bg-gray-700 hover:bg-gray-600 text-white'
                    }`}
                  >
                    <Trash2 className="h-5 w-5" />
                    <span>{showDeleteConfirm ? 'Confirm Delete Account' : 'Delete Account'}</span>
                  </button>
                  
                  {showDeleteConfirm && (
                    <div className="bg-red-500/20 border border-red-500/50 rounded-lg p-3">
                      <p className="text-red-400 text-sm">
                        This action cannot be undone. All your data will be permanently deleted.
                      </p>
                    </div>
                  )}
                </div>
              </div>

              <div className="bg-gray-800/50 rounded-lg p-6">
                <h3 className="text-lg font-semibold text-white mb-4">Account Information</h3>
                <div className="space-y-3 text-sm">
                  <div>
                    <span className="text-gray-400">Member since:</span>
                    <span className="text-white ml-2">Recently</span>
                  </div>
                  <div>
                    <span className="text-gray-400">Reading history:</span>
                    <span className="text-white ml-2">{lastReadChapters.length} series</span>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

      {showClearHistoryConfirm && (
        <div className="fixed inset-0 bg-black/50 flex items-center justify-center z-50">
          <div className="bg-gray-800 rounded-lg p-6 max-w-md w-full mx-4">
            <div className="flex items-center space-x-3 mb-4">
              <AlertTriangle className="h-6 w-6 text-yellow-500" />
              <h3 className="text-lg font-semibold text-white">Clear Reading History</h3>
            </div>
            
            <p className="text-gray-300 mb-6">
              Are you sure you want to clear all your reading history? This action cannot be undone and will remove all your progress tracking.
            </p>
            
            <div className="flex space-x-3">
              <button
                onClick={() => setShowClearHistoryConfirm(false)}
                className="flex-1 px-4 py-2 bg-gray-700 text-white rounded-lg hover:bg-gray-600 transition-colors"
              >
                Cancel
              </button>
              <button
                onClick={handleClearHistory}
                className="flex-1 px-4 py-2 bg-red-600 text-white rounded-lg hover:bg-red-700 transition-colors"
              >
                Clear History
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
