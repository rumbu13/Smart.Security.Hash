namespace Smart.Security.Hash
{
    public interface IPasswordHasher
    {
        /// <summary>
        /// Verify the specified password for the specified salt and hash
        /// </summary>
        /// <param name="password">Password to check</param>
        /// <param name="salt">Random salt used to generate the hash</param>
        /// <param name="hash">Calculated hash</param>
        /// <returns>true if the hash corresponds to the specified password, <see cref="false"/> otherwise</returns>
        bool Check(string password, byte[] salt, byte[] hash);

        /// <summary>
        /// Hashes the specified password.
        /// </summary>
        /// <param name="password">Password to hash</param>
        /// <returns>A tuple containing a random salt and a calculated hash</returns>
        (byte[] salt, byte[] hash) Hash(string password);
    }
}