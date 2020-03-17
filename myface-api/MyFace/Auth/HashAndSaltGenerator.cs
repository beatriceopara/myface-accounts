using System;
using System.Security.Cryptography;
using MyFace.Data;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RandomNumberGenerator = System.Security.Cryptography.RandomNumberGenerator;

namespace MyFace.Auth
{
    public static class HashAndSaltGenerator
    {
     
        //method - salt the password 
        public static byte[] GetSalt()
        {
            byte [] salt = new byte[128/ 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }

        //method - hash the password 
        public static string HashPassword(byte[] salt, string password)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt:salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 1000,
                numBytesRequested:256/8));
        }

    }
}