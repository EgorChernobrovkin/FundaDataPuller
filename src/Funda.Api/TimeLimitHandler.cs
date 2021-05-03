using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ComposableAsync;
using RateLimiter;

namespace Funda.Api
{
    public class TimeLimitHandler: DelegatingHandler
    {
        private readonly TimeLimiter _timeLimiter;

        public TimeLimitHandler(int limitPerMinute)
        {
            _timeLimiter = TimeLimiter.GetFromMaxCountByInterval(limitPerMinute, TimeSpan.FromMinutes(1));
        }
    
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            await _timeLimiter;
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
