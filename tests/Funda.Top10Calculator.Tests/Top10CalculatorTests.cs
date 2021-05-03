using System;
using System.Collections.Generic;
using System.Linq;
using Funda.Domain;
using Xunit;

namespace Funda.Top10Calculator.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void CalculateTop10Agents()
        {
            var objects = new List<RealEstate>();
            for (var i = 0; i < 7; i++)
            {
                objects.Add(new RealEstate()
                    {Id = Guid.NewGuid().ToString(), RealEstateAgentId = 4, RealEstateAgentName = "Agent4"});
            }
            
            for (var i = 0; i < 10; i++)
            {
                objects.Add(new RealEstate()
                    {Id = Guid.NewGuid().ToString(), RealEstateAgentId = 1, RealEstateAgentName = "Agent1"});
            }
            
            for (var i = 0; i < 1; i++)
            {
                objects.Add(new RealEstate()
                    {Id = Guid.NewGuid().ToString(), RealEstateAgentId = 10, RealEstateAgentName = "Agent10"});
            }
            
            for (var i = 0; i < 8; i++)
            {
                objects.Add(new RealEstate()
                    {Id = Guid.NewGuid().ToString(), RealEstateAgentId = 3, RealEstateAgentName = "Agent3"});
            }
            
            for (var i = 0; i < 9; i++)
            {
                objects.Add(new RealEstate()
                    {Id = Guid.NewGuid().ToString(), RealEstateAgentId = 2, RealEstateAgentName = "Agent2"});
            }

            for (var i = 0; i < 6; i++)
            {
                objects.Add(new RealEstate()
                    {Id = Guid.NewGuid().ToString(), RealEstateAgentId = 5, RealEstateAgentName = "Agent5"});
            }
            
            for (var i = 0; i < 4; i++)
            {
                objects.Add(new RealEstate()
                    {Id = Guid.NewGuid().ToString(), RealEstateAgentId = 7, RealEstateAgentName = "Agent7"});
            }
            
            for (var i = 0; i < 5; i++)
            {
                objects.Add(new RealEstate()
                    {Id = Guid.NewGuid().ToString(), RealEstateAgentId = 6, RealEstateAgentName = "Agent6"});
            }

            for (var i = 0; i < 3; i++)
            {
                objects.Add(new RealEstate()
                    {Id = Guid.NewGuid().ToString(), RealEstateAgentId = 8, RealEstateAgentName = "Agent8"});
            }
            
            for (var i = 0; i < 2; i++)
            {
                objects.Add(new RealEstate()
                    {Id = Guid.NewGuid().ToString(), RealEstateAgentId = 9, RealEstateAgentName = "Agent9"});
            }

            var top10Calculator = new Top10Calculator.Impl.Top10Calculator();

            var result = top10Calculator.CalculateTop10Agents(objects);
            var top10Agents = result.Agents.ToArray();
            Assert.Equal("Agent1", top10Agents[0].RealEstateAgentName);
            Assert.Equal("Agent2", top10Agents[1].RealEstateAgentName);
            Assert.Equal("Agent3", top10Agents[2].RealEstateAgentName);
            Assert.Equal("Agent4", top10Agents[3].RealEstateAgentName);
            Assert.Equal("Agent5", top10Agents[4].RealEstateAgentName);
            Assert.Equal("Agent6", top10Agents[5].RealEstateAgentName);
            Assert.Equal("Agent7", top10Agents[6].RealEstateAgentName);
            Assert.Equal("Agent8", top10Agents[7].RealEstateAgentName);
            Assert.Equal("Agent9", top10Agents[8].RealEstateAgentName);
            Assert.Equal("Agent10", top10Agents[9].RealEstateAgentName);
        }
    }
}
