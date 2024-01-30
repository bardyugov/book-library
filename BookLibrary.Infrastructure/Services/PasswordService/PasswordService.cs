using System.Security.Cryptography;
using System.Text;
using BookLibrary.Application.Services;

namespace BookLibrary.Infrastructure.Services.PasswordService;

public class PasswordService : IPasswordService
{
    const int KeySize = 64;
    const int Iterations = 350000;

    public string Hash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(KeySize);
        var algo = HashAlgorithmName.SHA512;

        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            Iterations,
            algo,
            KeySize);

        return Convert.ToHexString(hash);
    }

    public bool Verify(string password, string hashPassword)
    {
        var salt = RandomNumberGenerator.GetBytes(KeySize);
        var algo = HashAlgorithmName.SHA512;
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, algo, KeySize);
        
        return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hashPassword));   
    }
}