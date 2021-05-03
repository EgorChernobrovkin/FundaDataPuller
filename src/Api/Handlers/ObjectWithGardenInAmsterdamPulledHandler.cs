using System;
using System.Threading.Tasks;
using Rebus.Handlers;
using ServiceBus.Contracts.Messages;

namespace Api.Handlers
{
    public class ObjectWithGardenInAmsterdamPulledHandler: IHandleMessages<ObjectsWithGardenInAmsterdamPulled>
    {
        public Task Handle(ObjectsWithGardenInAmsterdamPulled message)
        {
            Console.WriteLine("Message received: ObjectsWithGardenInAmsterdamPulled");
            return Task.CompletedTask;
        }
    }
}
