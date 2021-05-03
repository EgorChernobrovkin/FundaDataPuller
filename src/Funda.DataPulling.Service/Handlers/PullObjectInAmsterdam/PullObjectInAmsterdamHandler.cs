using System;
using System.Threading.Tasks;
using CacheRepository;
using DistributedCache.Keys;
using Funda.DataPulling.Interfaces;
using Rebus.Bus;
using Rebus.Handlers;
using ServiceBus.Contracts.Messages;
using ServiceBus.HandlingOmitting;

namespace Funda.DataPulling.Service.Handlers.PullObjectInAmsterdam
{
    // ReSharper disable once UnusedType.Global // used in Message bus handler
    public class PullObjectInAmsterdamHandler : IHandleMessages<PullObjectsInAmsterdam>
    {
        private readonly IBus _bus;
        private readonly IFundaDataPullingService _pullingService;
        private readonly IDistributedCacheRepository _cache;
        private readonly IMessageHandlingOmitManager _handlingOmitManager;

        public PullObjectInAmsterdamHandler(
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

        public async Task Handle(PullObjectsInAmsterdam message)
        {
            if (_handlingOmitManager.CheckIfItShouldBeOmitted(message))
            {
                await _bus.Publish(new PullingObjectsInAmsterdamWasOmitted());
                return;
            }

            var objectsInfo = await _pullingService.PullObjectInAmsterdam();
            await _cache.Update(DistributedCacheKeys.AllObjectsInAmsterdam, objectsInfo);
            await _bus.Publish(new ObjectsInAmsterdamPulled());
            _handlingOmitManager.MessageHandled(message, new PullObjectInAmsterdamOmitRule(TimeSpan.FromMinutes(2)));
        }
    }
}
