
using MangaReaderAPI.Data;
using MangaReaderAPI.DTOs;
using MangaReaderAPI.Models;
using MangaReaderAPI.Repositories;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace MangaReaderAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly PasswordHasherService _passwordHasher;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthService(IUserRepository userRepo, PasswordHasherService hasher, IJwtTokenService jwtTokenService)
        {
            _userRepo = userRepo;
            _passwordHasher = hasher;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<TokenDto> RegisterUser(AuthRegisterDto registerDto)
        {
            if (await _userRepo.GetUserByEmail(registerDto.Email) != null)
            {
                throw new Exception("Email already in use.");
            }

            if (await _userRepo.GetUserByUsername(registerDto.Username) != null)
            {
                throw new Exception("Username already in use.");
            }

            byte[] userSalt = _passwordHasher.GenerateSalt();
            string hashedPassword = _passwordHasher.HashPassword(registerDto.Password, userSalt);

            var user = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = hashedPassword,
                Salt = Convert.ToBase64String(userSalt),
            };

            await _userRepo.CreateUser(user);

            var token = _jwtTokenService.GenerateToken(user.Id.ToString(), user.Email);

            return new TokenDto
            {
                Token = token
            };
        }

        public async Task<TokenDto> LoginUser(AuthLoginDto loginDto)
        {
            var userData = await _userRepo.GetUserByEmail(loginDto.Email);
            if (userData == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            if (!_passwordHasher.VerifyPassword(loginDto.Password, userData.PasswordHash, userData.Salt))
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            var token = _jwtTokenService.GenerateToken(userData.Id.ToString(), userData.Email);

            return new TokenDto
            {
                Token = token
            };
        }
    }
}
