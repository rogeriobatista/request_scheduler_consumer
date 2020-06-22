using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using request_scheduler_consumer.Consumers;
using request_scheduler_consumer.Data.Context;
using request_scheduler_consumer.Data.Repositories;
using request_scheduler_consumer.Domain.MauticForms.Interfaces;
using request_scheduler_consumer.Domain.MauticForms.Services;

namespace request_scheduler_consumer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IMauticFormRepository), typeof(MauticFormRepository));
            services.AddScoped(typeof(IMauticFormService), typeof(MauticFormService));
            services.AddScoped(typeof(ISendMauticForm), typeof(SendMauticForm));

            services.AddControllers();

            services.AddDbContext<RequestSchedulerContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("PostgreSql")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISendMauticForm sendMauticForm)
        {
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

            sendMauticForm.Register();
        }
    }
}
