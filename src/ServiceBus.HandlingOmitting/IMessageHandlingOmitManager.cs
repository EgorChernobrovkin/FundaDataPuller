using ServiceBus.Contracts.Messages;

namespace ServiceBus.HandlingOmitting
{
    public interface IMessageHandlingOmitManager
    {
        bool CheckIfItShouldBeOmitted(IMessage message);

        void MessageHandled(IMessage message, IMessageHandlingOmitRule rule = null);
    }
}
