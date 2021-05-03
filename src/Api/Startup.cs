using Api.HubConfig;
using CacheRepository;
using Funda.Top10Calculator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Rebus.Config;
using Rebus.Persistence.FileSystem;
using Rebus.Routing.TypeBased;
using Rebus.ServiceProvider;
using Rebus.Transport.FileSystem;
using ServiceBus.Contracts.Messages;
using ServiceBus.Contracts.Settings;

namespace Api
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
            var messageBusSettings = Configuration.GetSection("MessageBusSettings").Get<MessageBusSettings>();
            
            services.AddControllersWithViews();
            services.AutoRegisterHandlersFromAssemblyOf<Startup>();
            services.AddRebus((configure, provider) => configure
                .Logging(l => l.MicrosoftExtensionsLogging(provider.GetRequiredService<ILoggerFactory>()))
                .Transport(t => t.UseFileSystem(messageBusSettings.TransportBaseDirectory, messageBusSettings.QueueNameToListen))
                .Routing(r => r.TypeBased()
                    .Map<PullObjectsInAmsterdam>(messageBusSettings.QueueNameToWrite)
                    .Map<PullObjectsWithGardenInAmsterdam>(messageBusSettings.QueueNameToWrite)
                    .Map<ObjectsInAmsterdamPulled>(messageBusSettings.QueueNameToListen)
                    .Map<ObjectsWithGardenInAmsterdamPulled>(messageBusSettings.QueueNameToListen))
                .Subscriptions(s => s.UseJsonFile(messageBusSettings.SubscriptionFilePath)));
            
            var cacheRepSettings = Configuration.GetSection("DistributedCacheRepositorySettings")
                .Get<DistributedCacheRepositorySettings>();
            services.AddDistributedCacheRepository(cacheRepSettings.CacheRepositoryPath);
            services.AddFundaTop10Calculator();
            services.AddSignalR();
            
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "../ClientApp/dist"; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.ApplicationServices.UseRebus(async bus =>
            {
                await bus.Subscribe<ObjectsInAmsterdamPulled>();
                await bus.Subscribe<ObjectsWithGardenInAmsterdamPulled>();
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapHub<Top10Hub>("/top10hub");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "..\\ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
