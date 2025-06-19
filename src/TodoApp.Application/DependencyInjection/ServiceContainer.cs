using System;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.Application.MappingImplementaton;
using TodoApp.Application.MappingInterface;
using TodoApp.Application.UseCaseImplementation;
using TodoApp.Application.UseCaseInterface;

namespace TodoApp.Application.DependencyInjection;

public static class ServiceContainer
{

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserMapper, UserMapper>();
        services.AddScoped<ITodoService, TodoService>();
        services.AddScoped<ITodoMapper, TodoMapper>();
        return services;
    }

}
