using System.Security.Cryptography;

namespace Smart.Security.Hash.Tests
{
    public class HashTests
    {
        [Theory]
        [InlineData("SHA1")]
        [InlineData("SHA256")]
        [InlineData("SHA384")]
        [InlineData("SHA512")]
        public void PasswordChecksHash(string name)
        {
            PasswordHasherOptions options = new PasswordHasherOptions();
            options.HashAlgorithmName = new HashAlgorithmName(name);
            PasswordHasher hasher = new PasswordHasher(options);
            var hash = hasher.Hash("password");
            Assert.True(hasher.Check("password", hash.salt, hash.hash));
        }

        [Fact]
        public void PasswordChecksUsingBase64()
        {
            var hasher = new PasswordHasher();
            var hash = hasher.Base64Hash("pwd");
            Assert.True(hasher.Base64Check("pwd", hash));
        }

        [Fact]
        public void PasswordChecksUsingHex()
        {
            var hasher = new PasswordHasher();
            var hash = hasher.HexHash("pwd2");
            Assert.True(hasher.HexCheck("pwd2", hash));
        }

        [Theory]
        [InlineData('=')]
        [InlineData('/')]
        [InlineData('+')]
        [InlineData('a')]
        [InlineData('A')]
        [InlineData('1')]
        public void ThrowsOnInvalidBase64Separator(char separator)
        {
            var hasher = new PasswordHasher();
            Assert.Throws<ArgumentException>(() => hasher.Base64Hash("pwd", separator));
            Assert.Throws<ArgumentException>(() => hasher.Base64Check("pwd", "pwd", separator));
        }

        [Theory]        
        [InlineData('a')]
        [InlineData('A')]
        [InlineData('1')]
        public void ThrowsOnInvalidHexSeparator(char separator)
        {
            var hasher = new PasswordHasher();
            Assert.Throws<ArgumentException>(() => hasher.HexHash("pwd", separator));
            Assert.Throws<ArgumentException>(() => hasher.HexCheck("pwd", "pwd", separator));
        }
    }


}