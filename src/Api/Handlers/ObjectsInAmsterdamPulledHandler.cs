using System;
using System.Threading.Tasks;
using Rebus.Handlers;
using Rebus.Retry.Simple;
using ServiceBus.Contracts.Messages;

namespace Api.Handlers
{
    public class ObjectsInAmsterdamPulledHandler: IHandleMessages<ObjectsInAmsterdamPulled>/*, IHandleMessages<IFailed<ObjectsInAmsterdamPulled>>*/
    {
        public Task Handle(ObjectsInAmsterdamPulled message)
        {
            Console.WriteLine("Message received: ObjectsInAmsterdamPulled");
            return Task.CompletedTask;
        }

        public Task Handle(IFailed<ObjectsInAmsterdamPulled> message)
        {
            throw new NotImplementedException();
        }
    }
}
