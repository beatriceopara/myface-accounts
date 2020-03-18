using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace MyFace.Services
{
    public interface IHashService
    {
        byte[] GetSalt();
        string HashPassword(byte[] salt, string password);
    }

    public class HashService : IHashService
    {
        //method - salt the password 
        public byte[] GetSalt()
        {
            var salt = new byte[128 / 8];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            return salt;
        }

        //method - hash the password 
        public string HashPassword(byte[] salt, string password)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                salt,
                KeyDerivationPrf.HMACSHA1,
                1000,
                256 / 8));
        }
    }
}