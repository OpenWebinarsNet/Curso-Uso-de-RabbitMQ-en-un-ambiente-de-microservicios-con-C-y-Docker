using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UsersManagement.Application.Interfaces;
using UsersManagement.CrossCutting.Assemblies;
using UsersManagement.Infrastructure;

namespace UsersManagement.Application
{
    public static class Startup
    {
        public static IServiceCollection InitApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(AssemblyUtil.GetCurrentAssemblies());
            services.InitInfrastructureLayer(configuration);
            services.AddScoped<IUsersApplication, UsersApplication>();
            return services;
        }
    }
}