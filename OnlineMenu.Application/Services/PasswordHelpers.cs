using System.Diagnostics.Contracts;
using static System.Text.Encoding;
using System.Security.Cryptography;

namespace OnlineMenu.Application.Services
{
    public static class PasswordHelpers
    {
        [Pure]
        public static (byte[] passwordHash, byte[] passwordSalt) CreatePasswordHash(string password)
        {
            using var hmac = new HMACSHA512(); // Generates a random key (salt)
            var passwordSalt = hmac.Key;
            var passwordHash = hmac.ComputeHash(UTF8.GetBytes(password));
            return (passwordHash, passwordSalt);
        }
        
        [Pure]
        public static bool VerifyPasswordHash(string password, byte[] storedPasswordHash, byte[] storedSalt)
        {
            using var hmac = new HMACSHA512(storedSalt);
            var computedHash = hmac.ComputeHash(UTF8.GetBytes(password));

            for (var i = 0; i < computedHash.Length; i++)
                if (computedHash[i] != storedPasswordHash[i]) return false;
            return true;
        }
    }
}