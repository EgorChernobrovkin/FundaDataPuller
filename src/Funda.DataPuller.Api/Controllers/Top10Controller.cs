using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CacheRepository;
using DistributedCache.Keys;
using Funda.Domain;
using Funda.Top10Calculator.Interfaces;
using Funda.Top10Calculator.Models;
using Microsoft.AspNetCore.Mvc;
using Rebus.Bus;
using ServiceBus.Contracts.Messages;

namespace Funda.DataPuller.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Top10Controller : ControllerBase
    {
        private readonly IBus _bus;
        private readonly IDistributedCacheRepository _distributedCacheRepository;
        private readonly ITop10Calculator _top10Calculator;

        public Top10Controller(
            IBus bus, 
            IDistributedCacheRepository distributedCacheRepository, 
            ITop10Calculator top10Calculator)
        {
            _bus = bus;
            _distributedCacheRepository = distributedCacheRepository;
            _top10Calculator = top10Calculator;
        }

        [HttpPut("All")]
        public Task RequestTop10()
        {
            return _bus.Send(new PullObjectsInAmsterdam());
        }

        [HttpPut("ByGarden")]
        public Task RequestTop10ByGarden()
        {
            return _bus.Send(new PullObjectsWithGardenInAmsterdam());
        }

        [HttpGet("getcached")]
        public async Task<IReadOnlyCollection<RealEstate>> Get()
        {
            var exists = await _distributedCacheRepository.DoesExist(DistributedCacheKeys.AllObjectsInAmsterdam);
            if (!exists)
                return Array.Empty<RealEstate>();

            var res = await _distributedCacheRepository.Get<IReadOnlyCollection<RealEstate>>(DistributedCacheKeys.AllObjectsInAmsterdam);
            return res.ToArray();
        }

        [HttpGet("All")]
        public async Task<Top10Agents> GetTop10RealEstateAgents()
        {
            var exists = await _distributedCacheRepository.DoesExist(DistributedCacheKeys.AllObjectsInAmsterdam);
            if (!exists)
                return new Top10Agents(ArraySegment<RealEstateAgent>.Empty); // TODO: better answer is needed

            var res = await _distributedCacheRepository.Get<IReadOnlyCollection<RealEstate>>(DistributedCacheKeys.AllObjectsInAmsterdam);
            return _top10Calculator.CalculateTop10Agents(res);
        }

        [HttpGet("ByGarden")]
        public async Task<Top10Agents> GetTop10RealEstateAgentsByGarden()
        {
            var exists = await _distributedCacheRepository.DoesExist(DistributedCacheKeys.AllObjectsInAmsterdamWithGarden);
            if (!exists)
                return new Top10Agents(ArraySegment<RealEstateAgent>.Empty); // TODO: better answer is needed

            var res = await _distributedCacheRepository.Get<IReadOnlyCollection<RealEstate>>(DistributedCacheKeys.AllObjectsInAmsterdamWithGarden);
            return _top10Calculator.CalculateTop10Agents(res);
        }
    }
}
