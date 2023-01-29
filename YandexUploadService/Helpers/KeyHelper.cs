using System.Security.Cryptography;
using System.Text;
using System;
using System.Collections.Generic;

namespace YandexUploadService.Helpers
{
    public static class KeyHelper
    {

        public static string GenerateKey(string publicKey)
        {
            if (String.IsNullOrEmpty(publicKey))
                return String.Empty;

            String secretKey = new Random().Next().ToString();
            byte[] bkey = Encoding.Default.GetBytes(secretKey);
            String key = String.Empty;

            using (var hmac = new HMACSHA1(bkey))
            {
                byte[] bstr = Encoding.Default.GetBytes(publicKey);
                key = Convert.ToBase64String(hmac.ComputeHash(bstr));
            }

            return key;
        }
    }
}
