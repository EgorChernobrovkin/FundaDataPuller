using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Funda.Api.RealEstate.DTOs;
using Funda.Api.RealEstate.Interfaces;

namespace Funda.Api.RealEstate.Impl
{
    internal class FundaRealEstateRequester: IFundaRealEstateRequester, IDisposable
    {
        private readonly HttpClient _httpClient;

        public FundaRealEstateRequester(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient(ApiClientsConst.FundaApi);
        }
        
        public Task<RealEstateResult> Request(string urlParams)
        {
            return _httpClient.GetFromJsonAsync<RealEstateResult>(urlParams);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
