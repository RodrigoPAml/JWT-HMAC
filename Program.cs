using JWT_HMAC.Entities;
using JWT_HMAC.Providers;
using System.Security.Cryptography;
using System.Text.Json;

class Program
{
    static void HmacTest()
    {
        Console.WriteLine($"HMAC TEST");

        string key = PrivateKeyProvider.GenerateRandomKey(32);
        string content = "Your content here";
        string signed = HmacProvider.ComputeHash(key, content, HashAlgorithmName.SHA256);

        Console.WriteLine($"Key: {key}");
        Console.WriteLine($"Hash: {signed}");

        Console.WriteLine($"Check if signed ? {HmacProvider.ComputeHash(key, content, HashAlgorithmName.SHA256) == signed}");
        Console.WriteLine($"Check if signed with wrong key ? {HmacProvider.ComputeHash(key + "1", content, HashAlgorithmName.SHA256) == signed}");
        Console.WriteLine($"Check if signed with diferent payload ? {HmacProvider.ComputeHash(key, content + "1", HashAlgorithmName.SHA256) == signed}");
    }

    static void JwtTest()
    {
        Console.WriteLine($"JWT TEST");

        var payload = new PayloadTest()
        {
            UserId = 10,
            Email = "123@gmail.com"
        };

        payload.AddExpirationDate(DateTime.Now.AddDays(1));

        var key = PrivateKeyProvider.GenerateRandomKey(32);
        var token = JwtProvider.CreateToken(key, payload);

        Console.WriteLine($"Key: {key}");
        Console.WriteLine($"Token: {token}");
        Console.WriteLine($"Verify token: {JwtProvider.ValidateToken<PayloadTest>(token!, key) != null}");
        Console.WriteLine($"Payload: {JsonSerializer.Serialize(JwtProvider.ValidateToken<PayloadTest>(token!, key))}");

        Console.WriteLine($"Verify token with wrong key: {JwtProvider.ValidateToken<PayloadTest>(token!, key + "1") != null}");
        Console.WriteLine($"Verify token with invalid token: {JwtProvider.ValidateToken<PayloadTest>(token! + "1", key) != null}");
    }

    static void Main()
    {
        HmacTest();
        Console.WriteLine("\n---------------------------\n");
        JwtTest();
    }
}
