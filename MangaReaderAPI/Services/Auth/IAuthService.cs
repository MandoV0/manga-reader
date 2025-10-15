
using MangaReaderAPI.DTOs;
using System.Threading.Tasks;

namespace MangaReaderAPI.Services
{
    public interface IAuthService
    {
        Task<TokenDto> RegisterUser(RegisterRequestDto registerDto);
        Task<TokenDto> LoginUser(LoginRequestDto loginDto);

        Task RequestPasswordResetAsync(string email);
        Task ResetPasswordAsync(string token, string email, string newPassword);
        Task ChangePasswordAsync(string email, string currentPassword, string newPassword);
    }
}
