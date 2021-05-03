using CacheRepository;
using Funda.Api;
using Funda.Api.Settings;
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

namespace Funda.DataPulling.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            host.Services.UseRebus();

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var fundaApiInfo = hostContext.Configuration.GetSection("FundaApiInfo").Get<FundaApiInfo>();
                    services.AddFundaApiModule(fundaApiInfo.BaseURL.Replace("{key}", fundaApiInfo.AccessKey), fundaApiInfo.RequestsLimit);
                    services.AddFundaPullingLogic();

                    var cacheRepSettings = hostContext.Configuration.GetSection("DistributedCacheRepositorySettings")
                        .Get<DistributedCacheRepositorySettings>();
                    services.AddDistributedCacheRepository(cacheRepSettings.CacheRepositoryPath);
                    
                    var messageBusSettings = hostContext.Configuration.GetSection("MessageBusSettings").Get<MessageBusSettings>();
                    services.AutoRegisterHandlersFromAssemblyOf<Program>();
                    services.AddRebus((configure, provider) => configure
                        .Logging(l => l.MicrosoftExtensionsLogging(provider.GetRequiredService<ILoggerFactory>()))
                        .Transport(t => t.UseFileSystem(messageBusSettings.TransportBaseDirectory, messageBusSettings.QueueNameToListen))
                        .Subscriptions(s => s.UseJsonFile(messageBusSettings.SubscriptionFilePath))
                        .Routing(r => r.TypeBased()
                            .Map<ObjectsInAmsterdamPulled>(messageBusSettings.QueueNameToWrite))
                        .Options(o =>
                        {
                            o.SetNumberOfWorkers(1);
                            o.SetMaxParallelism(1);
                        }));
                });
    }
}
