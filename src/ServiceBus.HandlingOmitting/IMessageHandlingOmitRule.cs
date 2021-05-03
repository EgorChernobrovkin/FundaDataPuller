using System;
using ServiceBus.Contracts.Messages;

namespace ServiceBus.HandlingOmitting
{
    public interface IMessageHandlingOmitRule
    {
        void MessageWasHandled(IMessage message);
        
        bool ShouldItBeOmitted();
    }
}
