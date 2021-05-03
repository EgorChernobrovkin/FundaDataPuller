using CacheRepository;
using Funda.DataPuller.Api.HubConfig;
using Funda.Top10Calculator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Rebus.Config;
using Rebus.Persistence.FileSystem;
using Rebus.Routing.TypeBased;
using Rebus.ServiceProvider;
using Rebus.Transport.FileSystem;
using ServiceBus.Contracts.Messages;
using ServiceBus.Contracts.Settings;

namespace Funda.DataPuller.Api
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
            
            services.AutoRegisterHandlersFromAssemblyOf<Startup>();
            services.AddRebus((configure, provider) => configure
                .Logging(l => l.MicrosoftExtensionsLogging(provider.GetRequiredService<ILoggerFactory>()))
                .Transport(t => t.UseFileSystem(messageBusSettings.TransportBaseDirectory, messageBusSettings.QueueNameToListen))
                .Routing(r => r.TypeBased()
                    .Map<PullObjectsInAmsterdam>(messageBusSettings.QueueNameToWrite)
                    .Map<PullObjectsWithGardenInAmsterdam>(messageBusSettings.QueueNameToWrite)
                    .Map<ObjectsInAmsterdamPulled>(messageBusSettings.QueueNameToListen)
                    .Map<ObjectsWithGardenInAmsterdamPulled>(messageBusSettings.QueueNameToListen)
                    .Map<PullingObjectsInAmsterdamWasOmitted>(messageBusSettings.QueueNameToListen)
                    .Map<PullingObjectsWithGardenInAmsterdamWasOmitted>(messageBusSettings.QueueNameToListen))
                .Subscriptions(s => s.UseJsonFile(messageBusSettings.SubscriptionFilePath)));
            
            var cacheRepSettings = Configuration.GetSection("DistributedCacheRepositorySettings")
                .Get<DistributedCacheRepositorySettings>();
            services.AddDistributedCacheRepository(cacheRepSettings.CacheRepositoryPath);
            services.AddFundaTop10Calculator();
            services.AddSignalR();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Funda.DataPuller.Api", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Funda.DataPuller.Api v1"));
            }
            
            app.ApplicationServices.UseRebus(async bus =>
            {
                await bus.Subscribe<ObjectsInAmsterdamPulled>();
                await bus.Subscribe<ObjectsWithGardenInAmsterdamPulled>();
                await bus.Subscribe<PullingObjectsInAmsterdamWasOmitted>();
                await bus.Subscribe<PullingObjectsWithGardenInAmsterdamWasOmitted>();
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<Top10Hub>("/hub/top10");
            });
        }
    }
}
