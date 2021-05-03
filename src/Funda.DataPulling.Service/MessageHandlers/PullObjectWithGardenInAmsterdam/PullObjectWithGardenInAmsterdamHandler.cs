using System.Threading.Tasks;
using CacheRepository;
using DistributedCache.Keys;
using Funda.DataPulling.Interfaces;
using Rebus.Bus;
using Rebus.Handlers;
using ServiceBus.Contracts.Messages;
using ServiceBus.HandlingOmitting;

namespace Funda.DataPulling.Service.MessageHandlers.PullObjectWithGardenInAmsterdam
{
    // ReSharper disable once UnusedType.Global // used in Message bus handler
    public class PullObjectWithGardenInAmsterdamHandler: IHandleMessages<PullObjectsWithGardenInAmsterdam>
    {
        private readonly IBus _bus;
        private readonly IFundaDataPullingService _pullingService;
        private readonly IDistributedCacheRepository _cache;
        private readonly IMessageHandlingOmitManager _handlingOmitManager;

        public PullObjectWithGardenInAmsterdamHandler(
            IBus bus, 
            IFundaDataPullingService pullingService,
            IDistributedCacheRepository cache,
            IMessageHandlingOmitManager handlingOmitManager)
        {
            _bus = bus;
            _pullingService = pullingService;
            _cache = cache;
            _handlingOmitManager = handlingOmitManager;
        }
        
        public async Task Handle(PullObjectsWithGardenInAmsterdam message)
        {
            if (_handlingOmitManager.CheckIfItShouldBeOmitted(message))
            {
                await _bus.Publish(new PullingObjectsWithGardenInAmsterdamWasOmitted());
                return;
            }
            
            var objectsInfo = await _pullingService.PullObjectWithGardenInAmsterdam();
            await _cache.Update(DistributedCacheKeys.AllObjectsInAmsterdamWithGarden, objectsInfo);
            await _bus.Publish(new ObjectsWithGardenInAmsterdamPulled());
            _handlingOmitManager.MessageHandled(message);
        }
    }
}
