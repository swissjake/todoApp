using System;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.Application.MappingImplementaton;
using TodoApp.Application.MappingInterface;
using TodoApp.Application.Services;
using TodoApp.Application.UseCaseImplementation;
using TodoApp.Application.UseCaseInterface;
using TodoApp.Domain.Interfaces;

namespace TodoApp.Application.DependencyInjection;

public static class ServiceContainer
{

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserMapper, UserMapper>();
        services.AddScoped<ITodoService, TodoService>();
        services.AddScoped<ITodoMapper, TodoMapper>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IPasswordService, PasswordService>();
        return services;
    }

}
