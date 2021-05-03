using Microsoft.Extensions.DependencyInjection;

namespace ServiceBus.HandlingOmitting
{
    public static class MessageHandlingOmittingModule
    {
        public static void AddMessageHandlingOmitting(this IServiceCollection services)
        {
            services.AddSingleton<IMessageHandlingOmitManager, MessageHandlingOmitManager>();
        }
    }
}
