using System.Collections.Generic;
using System.Threading.Tasks;
using Funda.Domain;

namespace Funda.DataPulling.Interfaces
{
    public interface IFundaDataPullingService
    {
        Task<IReadOnlyCollection<RealEstate>> PullObjectInAmsterdam();

        Task<IReadOnlyCollection<RealEstate>> PullObjectWithGardenInAmsterdam();
    }
}
