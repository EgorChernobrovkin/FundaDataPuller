using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Funda.Api.RealEstate.DTOs
{
    public class RealEstateResult
    {
        public IReadOnlyCollection<RealEstate> Objects { get; set; }
        
        public Paging Paging { get; set; }
     
        [JsonPropertyName("TotaalAantalObjecten")]
        public int TotalNumberOfObjects { get; set; }
    }
}
