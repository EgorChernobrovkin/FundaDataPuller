namespace ServiceBus.Contracts.Settings
{
    public class MessageBusSettings
    {
        public string TransportBaseDirectory { get; set; }
        
        public string QueueNameToListen { get; set; }
        
        public string QueueNameToWrite { get; set; }
        
        public string SubscriptionFilePath { get; set; }
    }
}
