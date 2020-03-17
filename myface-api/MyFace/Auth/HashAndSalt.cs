using System;
using System.Security.Cryptography;
using MyFace.Data;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RandomNumberGenerator = System.Security.Cryptography.RandomNumberGenerator;

namespace MyFace.Auth
{
    public class HashAndSalt
    {
        //method - hash the password 

        public string PreparePasswordForDatabase(string password)
        {
            var saltedPassword = SaltPassword(password);
            var hashedPassword = HashPassword(saltedPassword);
          
            return (hashedPassword);
        }
       
        //method - hash the password 

        public string HashPassword(string password)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password:password,
                salt: Convert.FromBase64String(password),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 1000,
                numBytesRequested:256/8));
            return hashed;
        }
        
        //method - salt the password 
        public string SaltPassword(string password)
        {
            byte [] salt = new byte[128/ 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            Console.WriteLine($"Salt:{Convert.ToBase64String(salt)}");
            Console.WriteLine($"Password:{password}" + $"Salt:{salt}");   
            return password;
        }
        
    }
}