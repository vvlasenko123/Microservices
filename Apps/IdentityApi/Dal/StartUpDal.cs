using Dal.Repository.Models;
using Dal.Repository.Models.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Dal;

public static class StartUpDal
{
    public static IServiceCollection AddDal(this IServiceCollection services)
    {
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<IUserCredentialRepository, UserCredentialRepository>();
        services.AddSingleton<IUserTokenRepository, UserTokenRepository>();
        services.AddSingleton<IPasswordResetTokenRepository, PasswordResetTokenRepository>();

        return services;
    } 
}
