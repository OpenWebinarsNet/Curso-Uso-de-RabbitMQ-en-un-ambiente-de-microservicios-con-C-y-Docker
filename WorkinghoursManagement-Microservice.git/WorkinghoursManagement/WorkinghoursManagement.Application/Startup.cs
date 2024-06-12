using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkinghoursManagement.Application.Interfaces;
using WorkinghoursManagement.CrossCutting.Assemblies;
using WorkinghoursManagement.Infrastructure;

namespace WorkinghoursManagement.Application
{
    public static class Startup
    {
        public static IServiceCollection InitApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(AssemblyUtil.GetCurrentAssemblies());
            services.InitInfrastructureLayer(configuration);
            services.AddScoped<IWorkingHoursByUserApplication, WorkingHoursByUserApplication>();
            services.AddScoped<IUsersApplication, UsersApplication>();
            return services;
        }
    }
}