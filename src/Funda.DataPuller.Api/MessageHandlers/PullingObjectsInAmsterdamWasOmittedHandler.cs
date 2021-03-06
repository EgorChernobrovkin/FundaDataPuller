using System.Threading.Tasks;
using Funda.DataPuller.Api.HubConfig;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Rebus.Handlers;
using ServiceBus.Contracts.Messages;

namespace Funda.DataPuller.Api.MessageHandlers
{
    public class PullingObjectsInAmsterdamWasOmittedHandler: IHandleMessages<PullingObjectsInAmsterdamWasOmitted>
    {
        private readonly IHubContext<Top10Hub> _hub;
        private readonly ILogger<PullingObjectsInAmsterdamWasOmittedHandler> _logger;

        public PullingObjectsInAmsterdamWasOmittedHandler(IHubContext<Top10Hub> hub, ILogger<PullingObjectsInAmsterdamWasOmittedHandler> logger)
        {
            _hub = hub;
            _logger = logger;
        }
        
        public Task Handle(PullingObjectsInAmsterdamWasOmitted message)
        {
            _logger.LogInformation("Message received: PullingObjectsInAmsterdamWasOmitted");
            _logger.LogInformation("Sending information to the clients: transfer.pullingObjectsInAmsterdam.Omitted");
            return _hub.Clients.All.SendAsync("transfer.pullingObjectsInAmsterdam.Omitted");
        }
    }
}
