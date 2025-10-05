using System.Security.Cryptography;
using System.Text;
using Core.Models.Interfaces;

namespace Logic.Services;

public class PasswordHasher : IPasswordHasher
{
    public (string hash, string salt) Hash(string password)
    {
        var saltBytes = RandomNumberGenerator.GetBytes(16);
        var salt = Convert.ToBase64String(saltBytes);

        var hash = Compute(password, salt);
        return (hash, salt);
    }

    public bool Verify(string password, string hash, string salt)
    {
        var calc = Compute(password, salt);
        return SlowEquals(calc, hash);
    }

    private static string Compute(string password, string salt)
    {
        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password + salt);

        var hash = sha.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    private static bool SlowEquals(string a, string b)
    {
        var bytes = Encoding.UTF8.GetBytes(a);
        var bytes1 = Encoding.UTF8.GetBytes(b);

        if (bytes.Length != bytes1.Length)
        {
            return false;
        }

        var diff = 0;
        for (int i = 0; i < bytes.Length; i++)
        {
            diff |= bytes[i] ^ bytes1[i];
        }

        return diff == 0;
    }
}