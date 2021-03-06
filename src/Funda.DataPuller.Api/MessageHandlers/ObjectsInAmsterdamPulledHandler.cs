using System.Threading.Tasks;
using Funda.DataPuller.Api.HubConfig;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Rebus.Handlers;
using Rebus.Retry.Simple;
using ServiceBus.Contracts.Messages;

namespace Funda.DataPuller.Api.MessageHandlers
{
    public class ObjectsInAmsterdamPulledHandler: IHandleMessages<ObjectsInAmsterdamPulled>, IHandleMessages<IFailed<ObjectsInAmsterdamPulled>>
    {
        private readonly IHubContext<Top10Hub> _hub;
        private readonly ILogger<ObjectsInAmsterdamPulledHandler> _logger;

        public ObjectsInAmsterdamPulledHandler(IHubContext<Top10Hub> hub, ILogger<ObjectsInAmsterdamPulledHandler> logger)
        {
            _hub = hub;
            _logger = logger;
        }
        
        public Task Handle(ObjectsInAmsterdamPulled message)
        {
            _logger.LogInformation("Message received: ObjectsInAmsterdamPulled");
            _logger.LogInformation("Sending information to the clients: transfer.objectsInAmsterdamPulled.Success");
            return _hub.Clients.All.SendAsync("transfer.objectsInAmsterdamPulled.Success");
        }

        public Task Handle(IFailed<ObjectsInAmsterdamPulled> message)
        {
            _logger.LogInformation("Message received: ObjectsInAmsterdamPulled with error");
            _logger.LogInformation("Sending information to the clients: transfer.objectsInAmsterdamPulled.Error");
            return _hub.Clients.All.SendAsync("transfer.objectsInAmsterdamPulled.Error");
        }
    }
}
