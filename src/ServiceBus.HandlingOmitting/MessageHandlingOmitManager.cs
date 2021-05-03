using System;
using System.Collections.Generic;
using ServiceBus.Contracts.Messages;

namespace ServiceBus.HandlingOmitting
{
    public class MessageHandlingOmitManager: IMessageHandlingOmitManager
    {
        private readonly Dictionary<Type, IMessageHandlingOmitRule> _rules = new();
        
        public bool CheckIfItShouldBeOmitted(IMessage message)
        {
            var rule = _rules.GetValueOrDefault(message.GetType());
            return rule != null && rule.ShouldItBeOmitted();
        }

        public void MessageHandled(IMessage message, IMessageHandlingOmitRule rule = null)
        {
            foreach (var pair in _rules)
            {
                pair.Value.MessageWasHandled(message);
            }
            AddRule(message, rule);
        }

        private void AddRule(IMessage message, IMessageHandlingOmitRule rule = null)
        {
            if (rule != null)
                _rules[message.GetType()] = rule;
        }
    }
}
