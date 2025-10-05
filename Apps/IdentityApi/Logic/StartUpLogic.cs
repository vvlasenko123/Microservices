using Core.Models.Interfaces;
using Logic.Models;
using Logic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Logic;

public static class StartUpLogic
{
    public static IServiceCollection AddLogic(this IServiceCollection services)
    {
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<IUserService, UserService>();
        services.AddSingleton<IUserTokenService, UserTokenService>();
        services.AddSingleton<IPasswordResetService, PasswordResetService>();

        return services;
    } 
}
