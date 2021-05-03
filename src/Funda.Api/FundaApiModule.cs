using System;
using System.Net;
using Funda.Api.RealEstate.Impl;
using Funda.Api.RealEstate.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;

namespace Funda.Api
{
    public static class FundaApiModule
    {
        public static void AddFundaApiModule(
            this IServiceCollection services, 
            string baseAddress, 
            int limitPerMinute)
        {
            /*var handler = TimeLimiter.GetFromMaxCountByInterval(limitPerMinute, TimeSpan.FromMinutes(1)).AsDelegatingHandler();*/
            services
                .AddHttpClient(ApiClientsConst.FundaApi, client =>
                {
                    client.BaseAddress = new Uri(baseAddress);
                })
                .AddHttpMessageHandler(provider => new TimeLimitHandler(limitPerMinute))
                /*.AddHttpMessageHandler(() => handler)*/
                .AddPolicyHandler(message =>
                {
                    return HttpPolicyExtensions.HandleTransientHttpError().Or<TimeoutRejectedException>()
                        .OrResult(r => r.StatusCode is HttpStatusCode.TooManyRequests or HttpStatusCode.Unauthorized)
                        .CircuitBreakerAsync(1, TimeSpan.FromSeconds(10));
                });

            services.AddSingleton<IFundaRealEstateRequester, FundaRealEstateRequester>();
        }
    }
}
