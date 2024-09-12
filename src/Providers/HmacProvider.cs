using System.Security.Cryptography;
using System.Text;

namespace JWT_HMAC.Providers
{
    public static class HmacProvider
    {
        public static string ComputeHash(string key, string content, HashAlgorithmName algorithm)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var messageBytes = Encoding.UTF8.GetBytes(content);

            HMAC hmac = algorithm.Name switch
            {
                "SHA256" => new HMACSHA256(keyBytes),
                "SHA1" => new HMACSHA1(keyBytes),
                "MD5" => new HMACMD5(keyBytes),
                "SHA512" => new HMACSHA512(keyBytes),
                _ => throw new NotSupportedException($"Algorithm {algorithm.Name} is not supported")
            };

            var hashBytes = hmac.ComputeHash(messageBytes);

            return Convert.ToBase64String(hashBytes);
        }
    }
}
