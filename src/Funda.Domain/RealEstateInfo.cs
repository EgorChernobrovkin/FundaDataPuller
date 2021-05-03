using System;
using System.Collections.Generic;

namespace Funda.Domain
{
    public class RealEstateInfo
    {
        // ReSharper disable once UnusedMember.Global
        public RealEstateInfo()
        {
            // for caching
        }
        
        public RealEstateInfo(IReadOnlyCollection<RealEstate> objects, DateTime? dateTime = null)
        {
            Objects = objects;
            PulledDateTime = dateTime ?? DateTime.Now;
        }
        
        public IReadOnlyCollection<RealEstate> Objects { get; }
        
        public DateTime PulledDateTime { get; }
    }
}
