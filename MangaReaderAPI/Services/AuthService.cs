
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

        public AuthService(IUserRepository userRepo, PasswordHasherService hasher)
        {
            _userRepo = userRepo;
            _passwordHasher = hasher;
        }

        public async Task<UserDto> RegisterUser(AuthRegisterDto registerDto)
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

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
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

            // TODO: GENERATE JWT TOKEN!!!!!
            var token = "";
            throw new NotImplementedException();
        }
    }
}
