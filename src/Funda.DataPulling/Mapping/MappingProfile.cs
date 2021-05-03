using AutoMapper;
using RealEstate = Funda.Api.RealEstate.DTOs.RealEstate;

namespace Funda.DataPulling.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<RealEstate, Domain.RealEstate>();
        }
    }
}
