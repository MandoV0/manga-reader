using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace MangaReaderAPI.Services
{
    public class PasswordHasherService
    {
        public string HashPassword(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100_000,
                numBytesRequested: 256 / 8));
        }

        public byte[] GenerateSalt()
        {
            return RandomNumberGenerator.GetBytes(128 / 8);
        }

        /// <summary>
        /// Verifies if the given plain text password matches the stored password hash using the stored user salt.
        /// </summary>
        /// <param name="password">The plain text password to verify.</param>
        /// <param name="userPasswordHash">The stored password hash to compare against.</param>
        /// <param name="userSalt">The stored salt used to hash the password.</param>
        /// <returns>
        /// True if the hashed password matches the stored hash, otherwise false.
        /// </returns>
        public bool VerifyPassword(string password, string userPasswordHash, string userSalt)
        {
            byte[] saltBytes = Convert.FromBase64String(userSalt);
            string hashedPassword = HashPassword(password, saltBytes);
            return hashedPassword == userPasswordHash;
        }
    }
    
    

}