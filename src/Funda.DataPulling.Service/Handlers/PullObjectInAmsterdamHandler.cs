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
    public class PullObjectInAmsterdamHandler: IHandleMessages<PullObjectsInAmsterdam>
    {
        private readonly IBus _bus;
        private readonly IFundaDataPullingService _pullingService;
        private readonly IDistributedCacheRepository _cache;

        public PullObjectInAmsterdamHandler(
            IBus bus, 
            IFundaDataPullingService pullingService,
            IDistributedCacheRepository cache)
        {
            _bus = bus;
            _pullingService = pullingService;
            _cache = cache;
        }
        
        public async Task Handle(PullObjectsInAmsterdam message)
        {
            var objectsInfo = await _pullingService.PullObjectInAmsterdam();
            await _cache.Update(DistributedCacheKeys.AllObjectsInAmsterdam, objectsInfo);
            await _bus.Publish(new ObjectsInAmsterdamPulled());
        }
    }
}
