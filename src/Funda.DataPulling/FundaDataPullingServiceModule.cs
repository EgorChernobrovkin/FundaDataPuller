using Funda.DataPulling.Impl;
using Funda.DataPulling.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Funda.DataPulling
{
    public static class FundaDataPullingServiceModule
    {
        public static void AddFundaPullingLogic(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(FundaDataPullingServiceModule));
            services.AddScoped<IFundaDataPullingService, FundaDataPullingService>();
        }
    }
}
