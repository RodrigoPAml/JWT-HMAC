using System.Security.Cryptography;

namespace JWT_HMAC.Providers
{
    public static class PrivateKeyProvider
    {
        public static string GenerateRandomKey(int keyLengthInBytes)
        {
            byte[] key = new byte[keyLengthInBytes];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(key);
            }

            return Convert.ToBase64String(key);
        }
    }
}
