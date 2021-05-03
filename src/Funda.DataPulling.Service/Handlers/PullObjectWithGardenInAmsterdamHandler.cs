using System.Threading.Tasks;
using CacheRepository;
using DistributedCache.Keys;
using Funda.DataPulling.Interfaces;
using Rebus.Bus;
using Rebus.Handlers;
using ServiceBus.Contracts.Messages;

namespace Funda.DataPulling.Service.Handlers
{
    // ReSharper disable once UnusedType.Global // used in Message bus handler
    public class PullObjectWithGardenInAmsterdamHandler: IHandleMessages<PullObjectsWithGardenInAmsterdam>
    {
        private readonly IBus _bus;
        private readonly IFundaDataPullingService _pullingService;
        private readonly IDistributedCacheRepository _cache;

        public PullObjectWithGardenInAmsterdamHandler(
            IBus bus, 
            IFundaDataPullingService pullingService,
            IDistributedCacheRepository cache)
        {
            _bus = bus;
            _pullingService = pullingService;
            _cache = cache;
        }
        
        public async Task Handle(PullObjectsWithGardenInAmsterdam message)
        {
            var objectsInfo = await _pullingService.PullObjectWithGardenInAmsterdam();
            await _cache.Update(DistributedCacheKeys.AllObjectsInAmsterdamWithGarden, objectsInfo);
            await _bus.Publish(new ObjectsWithGardenInAmsterdamPulled());
        }
    }
}
