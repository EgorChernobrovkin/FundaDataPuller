using System.Text.Json.Serialization;

namespace Funda.Api.RealEstate.DTOs
{
    public class RealEstate
    {
        public string Id { get; set; }
        
        [JsonPropertyName("MakelaarId")]
        public int RealEstateAgentId { get; set; }
        
        [JsonPropertyName("MakelaarNaam")]
        public string RealEstateAgentName { get; set; }
    }
}
