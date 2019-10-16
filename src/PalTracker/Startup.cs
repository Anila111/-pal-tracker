using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Steeltoe.CloudFoundry.Connector.MySql.EFCore;
using Steeltoe.Management.CloudFoundry;
using Steeltoe.Common.HealthChecks;
using Steeltoe.Management.Endpoint.Info;

namespace PalTracker
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton(sp => new WelcomeMessage(Configuration.GetValue<string>("WELCOME_MESSAGE", "WELCOME_MESSAGE configured.")));
            services.AddSingleton(sp => new CloudFoundryInfo(
              Configuration.GetValue<string>("PORT", "80"),  
              Configuration.GetValue<string>("MEMORY_LIMIT", "2GB"),  
              Configuration.GetValue<string>("CF_INSTANCE_INDEX", "1"),  
              Configuration.GetValue<string>("CF_INSTANCE_ADDR", "127.0.0.1")
          ));
          services.AddScoped<ITimeEntryRepository, MySqlTimeEntryRepository>();
          services.AddDbContext<TimeEntryContext>(options => options.UseMySql(Configuration));
          services.AddCloudFoundryActuators(Configuration);
          services.AddScoped<IHealthContributor, TimeEntryHealthContributor>();
          services.AddSingleton<IOperationCounter<TimeEntry>,OperationCounter<TimeEntry>>();
          services.AddSingleton<IInfoContributor, TimeEntryInfoContributor>();
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

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseCloudFoundryActuators();
        }
    }
}
