using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace Smart.Security.Hash
{
    public class PasswordHasherOptions
    {
        [Range(8, 128)]
        public int SaltSize { get; set; } = 16;
        [Range(8, 256)]
        public int HashSize { get; set; } = 32;

        [Range(1000, int.MaxValue)]
        public int Iterations { get; set; } = 10000;

        public HashAlgorithmName HashAlgorithmName { get; set; } = HashAlgorithmName.SHA256;




    }
}