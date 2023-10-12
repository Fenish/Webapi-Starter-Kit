using System.Security.Cryptography;
using System.Text;

namespace SessionAuthentication.Utils;

public class StringUtils
{

    public static string Sha256Encrypt(string value)
    {
        var bytes = Encoding.UTF8.GetBytes(value);
        var hash = SHA256.HashData(bytes);
        
        var builder = new StringBuilder();
        foreach (var t in hash)
        {
            builder.Append(t.ToString("x2"));
        }

        return builder.ToString();
    }
    
    public static string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}