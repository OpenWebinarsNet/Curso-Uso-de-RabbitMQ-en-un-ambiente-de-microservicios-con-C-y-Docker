using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using WorkinghoursManagement.Application;
using WorkinghoursManagement.Infrastructure.Extensions;
using WorkinghoursManagement.Infrastructure.MessageBroker.Listener;
using WorkingHoursManagement.Api.Jobs;

namespace WorkinghoursManagement.Api
{
    public class Startup
    {
        private const string SQLCNXNAMEHF = "SqlServerConnectionStringHF";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.InitApplicationLayer(Configuration);

            services.AddSingleton(typeof(IMemoryCache), typeof(MemoryCache));

            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

            services.AddSwaggerGen();

            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString(SQLCNXNAMEHF)));

            services.AddHangfireServer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ApplicationServices.GetService<UserOperationsListener>();
            app.UseRabbitMQ();
            app.UseSwagger();

            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "WorkinghoursManagement.Api");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHangfireDashboard();

            //BackgroundJob.Enqueue<CheckUsersWorkingHoursJob>(x => x.DoCheckAsync(CancellationToken.None));
            RecurringJob.RemoveIfExists(nameof(CheckUsersWorkingHoursJob));
            RecurringJob.AddOrUpdate<CheckUsersWorkingHoursJob>(nameof(CheckUsersWorkingHoursJob), x => x.DoCheckAsync(CancellationToken.None), Cron.Minutely);
        }
    }
}