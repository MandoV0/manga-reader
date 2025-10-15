
using MangaReaderAPI.Data;
using MangaReaderAPI.Exceptions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using MangaReaderAPI.Configuration;
using MangaReaderAPI.DTOs;
using MangaReaderAPI.DTOs.Auth;
using MangaReaderAPI.Repositories;
using MangaReaderAPI.Models;

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
        private readonly IForgotPasswordRepository _forgotPasswordRepo;
        private readonly IEmailService _emailService;
        private readonly FrontendSettings _frontendSettings;

        public AuthService(IUserRepository userRepo, IForgotPasswordRepository forgotPasswordRepo, PasswordHasherService hasher, IJwtTokenService jwtTokenService, IEmailService emailService, IOptions<FrontendSettings> frontendSettingsOptions)
        {
            _userRepo = userRepo;
            _passwordHasher = hasher;
            _jwtTokenService = jwtTokenService;
            _forgotPasswordRepo = forgotPasswordRepo;
            _emailService = emailService;
            _frontendSettings = frontendSettingsOptions.Value;
        }

        public async Task<TokenDto> RegisterUser(RegisterRequestDto registerDto)
        {
            if (await _userRepo.GetUserByEmail(registerDto.Email) != null)
            {
                throw new EmailAlreadyExistsException(registerDto.Email);
            }

            if (await _userRepo.GetUserByUsername(registerDto.Username) != null)
            {
                throw new UsernameAlreadyExistsException(registerDto.Username);
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

        public async Task<TokenDto> LoginUser(LoginRequestDto loginDto)
        {
            var userData = await _userRepo.GetUserByEmail(loginDto.Email);
            if (userData == null)
            {
                throw new InvalidCredentialsException();
            }

            if (!_passwordHasher.VerifyPassword(loginDto.Password, userData.PasswordHash, userData.Salt))
            {
                throw new InvalidCredentialsException();
            }

            var token = _jwtTokenService.GenerateToken(userData.Id.ToString(), userData.Email);

            return new TokenDto
            {
                Token = token
            };
        }

        public async Task RequestPasswordResetAsync(string email)
        {
            var user = await _userRepo.GetUserByEmail(email);
            if (user == null) return;

            var resetToken = Guid.NewGuid().ToString();
            var forgotPasswordEntry = new UserForgotPassword
            {
                UserId = user.Id,
                ResetToken = resetToken,
                Expiry = DateTime.UtcNow.AddHours(1),
                Used = false
            };

            var resetLink = $"{_frontendSettings.ResetPasswordUrl}?token={resetToken}&email={email}";
            var emailContent = $"<p>You requested a password reset. Click the link below to reset your password:</p><p><a href='{resetLink}'>Reset Password</a></p>";

            await _emailService.SendEmailAsync(email, "Password Reset", emailContent);

            await _forgotPasswordRepo.AddAsync(forgotPasswordEntry);
        }

        public async Task ResetPasswordAsync(string token, string email, string newPassword)
        {
            var user = await _userRepo.GetUserByEmail(email);
            if (user == null)
            {
                throw new InvalidCredentialsException();
            }

            var forgotPasswordEntry = await _forgotPasswordRepo.GetByTokenAsync(token);
            if (forgotPasswordEntry == null || forgotPasswordEntry.UserId != user.Id)
            {
                throw new InvalidPasswordResetTokenException();
            }

            if (forgotPasswordEntry.Expiry < DateTime.UtcNow)
            {
                throw new ExpiredPasswordResetTokenException();
            }

            if (forgotPasswordEntry.Used)
            {
                throw new InvalidPasswordResetTokenException();
            }

            byte[] userSalt = _passwordHasher.GenerateSalt();
            string hashedPassword = _passwordHasher.HashPassword(newPassword, userSalt);

            user.UpdatedAt = DateTime.UtcNow;
            user.PasswordHash = hashedPassword;
            user.Salt = Convert.ToBase64String(userSalt);

            await _userRepo.UpdateAsync(user);
            await _forgotPasswordRepo.DeleteAsync(forgotPasswordEntry);
        }

                public async Task ChangePasswordAsync(string email, string currentPassword, string newPassword)
        {
            var user = await _userRepo.GetUserByEmail(email);
            if (user == null)
            {
                throw new InvalidCredentialsException();
            }

            if (!_passwordHasher.VerifyPassword(currentPassword, user.PasswordHash, user.Salt))
            {
                throw new InvalidCredentialsException();
            }

            byte[] userSalt = _passwordHasher.GenerateSalt();
            string hashedPassword = _passwordHasher.HashPassword(newPassword, userSalt);

            user.UpdatedAt = DateTime.UtcNow;
            user.PasswordHash = hashedPassword;
            user.Salt = Convert.ToBase64String(userSalt);

            await _userRepo.UpdateAsync(user);
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _userRepo.GetUserById(userId);
            if (user == null)
            {
                return false;
            }

            await _userRepo.DeleteUserAsync(user);
            return true;
        }
    }
}