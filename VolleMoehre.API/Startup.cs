using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyCronJob.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using VolleMoehre.API.Jobs;

namespace VolleMoehre.API
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
            services.AddMvc(options => options.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.ApplyResulation<TrainingsReminderJob>(options =>
            {
                options.CronExpression = "0 12 * * *";
                options.TimeZoneInfo = TimeZoneInfo.Local;
                options.CronFormat = Cronos.CronFormat.Standard;
            });
            services.ApplyResulation<AuftrittsReminderJob>(options =>
            {
                options.CronExpression = "0 12 * * *";
                options.TimeZoneInfo = TimeZoneInfo.Local;
                options.CronFormat = Cronos.CronFormat.Standard;
            });

            //services.ApplyResulation<TimTest>(options =>
            //{
            //    options.CronExpression = "46 13 * * *";
            //    options.TimeZoneInfo = TimeZoneInfo.Local;
            //    options.CronFormat = Cronos.CronFormat.Standard;
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithOrigins("https://intern.vollemoehre.de", "https://localhost:64784"));

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
