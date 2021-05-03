﻿using System;
using System.Threading.Tasks;
using Api.HubConfig;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Rebus.Handlers;
using Rebus.Retry.Simple;
using ServiceBus.Contracts.Messages;

namespace Api.Handlers
{
    public class ObjectsWithGardenInAmsterdamPulledHandler: IHandleMessages<ObjectsWithGardenInAmsterdamPulled>, IHandleMessages<IFailed<ObjectsWithGardenInAmsterdamPulled>>
    {
        private readonly IHubContext<Top10Hub> _hub;
        private readonly ILogger<ObjectsInAmsterdamPulledHandler> _logger;

        public ObjectsWithGardenInAmsterdamPulledHandler(IHubContext<Top10Hub> hub, ILogger<ObjectsInAmsterdamPulledHandler> logger)
        {
            _hub = hub;
            _logger = logger;
        }
        
        public Task Handle(ObjectsWithGardenInAmsterdamPulled message)
        {
            _logger.LogInformation("Message received: ObjectsWithGardenInAmsterdamPulled");
            _logger.LogInformation("Sending information to the clients: transfer.objectsWithGardenInAmsterdamPulled.Success");
            return _hub.Clients.All.SendAsync("transfer.objectsWithGardenInAmsterdamPulled.Success");
        }

        public Task Handle(IFailed<ObjectsWithGardenInAmsterdamPulled> message)
        {
            _logger.LogInformation("Message received: ObjectsWithGardenInAmsterdamPulled with error");
            _logger.LogInformation("Sending information to the clients: transfer.objectsWithGardenInAmsterdamPulled.Error");
            return _hub.Clients.All.SendAsync("transfer.objectsWithGardenInAmsterdamPulled.Error");
        }
    }
}
