using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Smart.Security.Hash;
using System.Security.Cryptography;

namespace Smart.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPasswordHasher(this IServiceCollection services)
    {
        services.AddOptions<PasswordHasherOptions>()
            .Configure(options =>
            {
                options.SaltSize = 16;
                options.HashSize = 32;
                options.HashAlgorithmName = HashAlgorithmName.SHA256;
            });
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        return services;
    }

    public static IServiceCollection AddPasswordHasher(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PasswordHasherOptions>(configuration);
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        return services;
    }

    public static IServiceCollection AddPasswordHasher(this IServiceCollection services, string configSectionPath)
    {
        services.AddOptions<PasswordHasherOptions>()
            .BindConfiguration(configSectionPath)
            .ValidateDataAnnotations()
            .ValidateOnStart();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        return services;
    }

    public static IServiceCollection AddPasswordHasher(this IServiceCollection services, Action<PasswordHasherOptions> configureOptions)
    {
        services.Configure(configureOptions);
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        return services;
    }

    public static IServiceCollection AddPasswordHasher(this IServiceCollection services, PasswordHasherOptions userOptions)
    {
        services.AddOptions<PasswordHasherOptions>()
            .Configure(options =>
            {
                options.SaltSize = userOptions.SaltSize;
                options.HashSize = userOptions.HashSize;
                options.HashAlgorithmName = userOptions.HashAlgorithmName;
            });
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        return services;
    }
}
