using Microsoft.Extensions.DependencyInjection;
using MyFirstWebApp.Interfaces;
using MyFirstWebApp.Services;

namespace MyFirstWebApp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register all application services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IDropdownService, DropdownService>();
            
            return services;
        }
    }
}
