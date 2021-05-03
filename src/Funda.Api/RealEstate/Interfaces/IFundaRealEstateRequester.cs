using System.Threading.Tasks;
using Funda.Api.RealEstate.DTOs;

namespace Funda.Api.RealEstate.Interfaces
{
    public interface IFundaRealEstateRequester
    {
        Task<RealEstateResult> Request(string urlParams);
    }
}
