using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace CvMakerAi.Models
{
    public class Settings
    {
        public string HashPassword(string password)
        {
            // 16 baytlık Salt oluştur
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Şifreyi hashle
            byte[] hashed = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);

            // Hash ve Salt'ı birleştirip Base64 olarak sakla
            string hashedPassword = Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hashed);
            return hashedPassword;
        }

        // Şifre Doğrulama
        public bool VerifyPassword(string enteredPassword, string storedHash)
        {
            // Kaydedilen hash içindeki salt ve hash'i ayır
            string[] parts = storedHash.Split(':');
            if (parts.Length != 2)
                return false;

            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] storedHashBytes = Convert.FromBase64String(parts[1]);

            // Girilen şifreyi aynı salt ile hashle
            byte[] enteredHashBytes = KeyDerivation.Pbkdf2(
                password: enteredPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);

            // Hash'leri karşılaştır
            return CryptographicOperations.FixedTimeEquals(storedHashBytes, enteredHashBytes);
        }



    }
}
