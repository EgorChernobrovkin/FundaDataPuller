using System.Collections.Generic;
using System.Linq;
using Funda.Domain;
using Funda.Top10Calculator.Interfaces;
using Funda.Top10Calculator.Models;

namespace Funda.Top10Calculator.Impl
{
    internal class Top10Calculator : ITop10Calculator
    {
        public Top10Agents CalculateTop10Agents(IReadOnlyCollection<RealEstate> objects)
        {
            var agentsDictionary = new Dictionary<int, RealEstateAgent>();
            foreach (var obj in objects)
            {
                var savedAgent = agentsDictionary.GetValueOrDefault(obj.RealEstateAgentId);
                if (savedAgent != null)
                {
                    agentsDictionary[obj.RealEstateAgentId].NumberOfObjects++;
                    continue;
                }
                var agent = new RealEstateAgent(obj.RealEstateAgentId, obj.RealEstateAgentName)
                {
                    NumberOfObjects = 1
                };
                agentsDictionary[obj.RealEstateAgentId] = agent;
            }

            var top10Agents = agentsDictionary.Values
                .OrderByDescending(agent => agent.NumberOfObjects)
                .Take(10)
                .Select(agent => agent);
            return new Top10Agents(top10Agents.ToArray());
        }
    }
}
