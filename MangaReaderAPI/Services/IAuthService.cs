
using MangaReaderAPI.DTOs;
using System.Threading.Tasks;

namespace MangaReaderAPI.Services
{
    public interface IAuthService
    {
        Task<UserDto> RegisterUser(AuthRegisterDto registerDto);
        Task<TokenDto> LoginUser(AuthLoginDto loginDto);
    }
}
