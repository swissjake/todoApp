using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Interfaces;
using TodoApp.Application.UseCaseInterface;
using TodoApp.Application.UseCaseImplementation;
using TodoApp.Application.MappingInterface;
using TodoApp.Application.MappingImplementaton;
using TodoApp.Infrastructure.RepositoryImplementation;
using TodoApp.Infrastructure.Db;



namespace TodoApp.Infrastructure.DependencyInjection;


public static class ServiceContainer
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Default"), b => b.MigrationsAssembly("TodoApp.Infrastructure")));

        // Register Repository Implementations
        services.AddScoped<ITodoRepository, TodoRepository>();
        services.AddScoped<IUserRepository, UserRepository>();


        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserMapper, UserMapper>();
        services.AddScoped<ITodoService, TodoService>();
        services.AddScoped<ITodoMapper, TodoMapper>();
        return services;
    }
}
