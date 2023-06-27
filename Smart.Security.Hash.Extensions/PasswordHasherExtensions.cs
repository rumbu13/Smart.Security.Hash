namespace Smart.Security.Hash;

public static class PasswordHasherExtensions
{

    public static string Base64Hash(this IPasswordHasher hasher, string password, char separator = ';')
    {
        if (char.IsAsciiLetterOrDigit(separator) || separator == '+' || separator == '/' || separator == '=')
        {
            throw new ArgumentException($"Separator {separator} cannot be used for a Base64 string", nameof(separator));
        }
        var result = hasher.Hash(password);
        return string.Join(separator, Convert.ToBase64String(result.salt), Convert.ToBase64String(result.hash));
    }

    public static bool Base64Check(this IPasswordHasher hasher, string password, string hashInput, char separator = ';')
    {
        if (char.IsAsciiLetterOrDigit(separator) || separator == '+' || separator == '/' || separator == '=')
        {
            throw new ArgumentException($"Separator {separator} cannot be used for a Base64 string", nameof(separator));
        }
        var parts = hashInput.Split(separator);
        if (parts.Length != 2)
        {
            throw new InvalidDataException("The provided hash is invalid");
        }

        var salt = Convert.FromBase64String(parts[0]);
        var hash = Convert.FromBase64String(parts[1]);

        return hasher.Check(password, salt, hash);
    }

    public static string HexHash(this IPasswordHasher hasher, string password, char separator = ';')
    {
        if (char.IsAsciiHexDigit(separator))
        {
            throw new ArgumentException($"Separator {separator} cannot be used for a hex string", nameof(separator));
        }
        var result = hasher.Hash(password);
        return string.Join(separator, Convert.ToHexString(result.salt), Convert.ToHexString(result.hash));
    }

    public static bool HexCheck(this IPasswordHasher hasher, string password, string hashInput, char separator = ';')
    {
        if (char.IsAsciiHexDigit(separator))
        {
            throw new ArgumentException($"Separator {separator} cannot be used for a Base64 string", nameof(separator));
        }
        var parts = hashInput.Split(separator);
        if (parts.Length != 2)
        {
            throw new InvalidDataException("The provided hash is invalid");
        }

        var salt = Convert.FromHexString(parts[0]);
        var hash = Convert.FromHexString(parts[1]);

        return hasher.Check(password, salt, hash);
    }

}
