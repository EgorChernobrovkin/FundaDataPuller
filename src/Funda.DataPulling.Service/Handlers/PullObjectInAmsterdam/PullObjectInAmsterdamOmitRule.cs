using System;
using ServiceBus.Contracts.Messages;
using ServiceBus.HandlingOmitting;

namespace Funda.DataPulling.Service.Handlers.PullObjectInAmsterdam
{
    public class PullObjectInAmsterdamOmitRule: IMessageHandlingOmitRule
    {
        private readonly DateTime _expiration;
        private bool _anotherMessageWasHandled;


        public PullObjectInAmsterdamOmitRule(TimeSpan expiration)
        {
            _expiration = DateTime.Now.Add(expiration);
        }

        public void MessageWasHandled(IMessage message)
        {
            // every message handling drops omitting
            _anotherMessageWasHandled = true;
        }

        public bool ShouldItBeOmitted()
        {
            return !_anotherMessageWasHandled && DateTime.Now < _expiration;
        }
    }
}
