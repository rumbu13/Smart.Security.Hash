using System.Security.Cryptography;

namespace Smart.Security.Hash;

public class PasswordHasher : IPasswordHasher
{    
    private readonly PasswordHasherOptions _options;

    public PasswordHasher(PasswordHasherOptions? options = default)
    {
        _options = options ?? new PasswordHasherOptions();
    }

    public (byte[] salt, byte[] hash) Hash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(_options!.SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, _options.Iterations, _options.HashAlgorithmName, _options.HashSize);

        return (salt, hash);
    }
    
    public bool Check(string password, byte[] salt, byte[] hash)
    {
        var generatedHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, _options.Iterations, _options.HashAlgorithmName, _options.HashSize);
        return CryptographicOperations.FixedTimeEquals(hash, generatedHash);

    }
}
