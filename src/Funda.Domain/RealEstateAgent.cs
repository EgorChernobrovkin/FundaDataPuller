namespace Funda.Domain
{
    public class RealEstateAgent
    {
        public RealEstateAgent(int realEstateAgentId, string realEstateAgentName)
        {
            RealEstateAgentId = realEstateAgentId;
            RealEstateAgentName = realEstateAgentName;
        }

        public int RealEstateAgentId { get; }
        
        public string RealEstateAgentName { get; }
        
        public int NumberOfObjects { get; set; }
    }
}
