namespace MangaReaderAPI.Services
{
    public interface IUserForgotPasswordService
    {
        Task RequestPasswordResetAsync(string email);
        Task ResetPasswordAsync(string token, string email, string newPassword);
    }
}