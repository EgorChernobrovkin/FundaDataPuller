using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Funda.Api.RealEstate.Interfaces;
using Funda.Api.Settings;
using Funda.DataPulling.Interfaces;
using Funda.Domain;
using Helpers;
using Microsoft.Extensions.Configuration;

namespace Funda.DataPulling.Impl
{
    internal class FundaDataPullingService : IFundaDataPullingService
    {
        private readonly IFundaRealEstateRequester _requester;
        private readonly IMapper _mapper;
        private readonly int _pageSize;

        public FundaDataPullingService(
            IFundaRealEstateRequester requester,
            IConfiguration configuration,
            IMapper mapper)
        {
            _requester = requester;
            _mapper = mapper;
            _pageSize = configuration.GetSection("FundaApiInfo").Get<FundaApiInfo>().PageSize;
        }

        public Task<IReadOnlyCollection<RealEstate>> PullObjectInAmsterdam()
        {
            return PullObjects("?type=koop&zo=/amsterdam/");
        }

        public Task<IReadOnlyCollection<RealEstate>> PullObjectWithGardenInAmsterdam()
        {
            return PullObjects("?type=koop&zo=/amsterdam/tuin/");
        }

        private async Task<IReadOnlyCollection<RealEstate>> PullObjects(string baseUrlParams)
        {
            var firstAttemptResult = await _requester.Request($"{baseUrlParams}&pagesize={_pageSize}");
            var firstBatch = _mapper.Map<RealEstate[]>(firstAttemptResult.Objects);
            if (firstAttemptResult.TotalNumberOfObjects <= _pageSize)
                return firstBatch;

            var result = new List<RealEstate>();
            result.AddRange(firstBatch);

            for (var page = 2; page <= firstAttemptResult.Paging.NumberOfPages; page++)
            {
                var pageRes = await _requester.Request(
                    $"{baseUrlParams}&page={page}&pagesize={_pageSize}");
                result.AddRange(_mapper.Map<RealEstate[]>(pageRes.Objects));
            }

            return result.Distinct(
                    new LambdaEqualityComparer<RealEstate>(
                        (x, y) => x.Id == y.Id,
                        realEstate => realEstate.Id.GetHashCode()))
                .ToArray();
        }
    }
}
