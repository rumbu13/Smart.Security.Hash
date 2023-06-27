using Microsoft.Extensions.Options;
using Smart.Security.Hash;

namespace Smart.Extensions.DependencyInjection;

internal class PasswordHasher: Smart.Security.Hash.PasswordHasher
{
    public PasswordHasher(IOptions<PasswordHasherOptions> options) : base(options.Value)
    {
    }
}
