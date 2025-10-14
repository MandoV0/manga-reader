
using Microsoft.AspNetCore.Mvc;
using MangaReaderAPI.Services;
using MangaReaderAPI.DTOs;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MangaReaderAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<ActionResult<TokenDto>> Register(AuthRegisterDto registerDto)
        {
            var token = await _service.RegisterUser(registerDto);
            return Ok(token);
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenDto>> Login(AuthLoginDto loginDto)
        {
            var token = await _service.LoginUser(loginDto);
            return Ok(token);
        }

        [HttpPost("forgot-password")]
        /// <summary>
        /// Calls Email Service to send a email for password reset to the user.
        /// </summary>
        public async Task ForgotPassword(string email)
        {
            
        }
    }
}
