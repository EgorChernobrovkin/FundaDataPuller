using System.Threading.Tasks;
using Funda.DataPuller.Api.HubConfig;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Rebus.Handlers;
using ServiceBus.Contracts.Messages;

namespace Funda.DataPuller.Api.Handlers
{
    public class PullingObjectsWithGardenInAmsterdamWasOmittedHandler: IHandleMessages<PullingObjectsWithGardenInAmsterdamWasOmitted>
    {
        private readonly IHubContext<Top10Hub> _hub;
        private readonly ILogger _logger;

        public PullingObjectsWithGardenInAmsterdamWasOmittedHandler(IHubContext<Top10Hub> hub, ILogger logger)
        {
            _hub = hub;
            _logger = logger;
        }
        
        public Task Handle(PullingObjectsWithGardenInAmsterdamWasOmitted message)
        {
            _logger.LogInformation("Message received: PullingObjectsWithGardenInAmsterdamWasOmitted");
            _logger.LogInformation("Sending information to the clients: transfer.pullingObjectsWithGardenInAmsterdam.Omitted");
            return _hub.Clients.All.SendAsync("transfer.pullingObjectsWithGardenInAmsterdam.Omitted");
        }
    }
}
