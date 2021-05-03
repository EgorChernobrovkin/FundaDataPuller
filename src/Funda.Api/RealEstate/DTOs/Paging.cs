using System.Text.Json.Serialization;

namespace Funda.Api.RealEstate.DTOs
{
    public class Paging
    {
        [JsonPropertyName("HuidigePagina")]
        public int CurrentPage { get; set; }
        
        [JsonPropertyName("AantalPaginas")]
        public int NumberOfPages { get; set; }
    }
}
