﻿using System.Collections.Generic;
using Funda.Domain;

namespace Funda.Top10Calculator.Entities
{
    public class Top10Agents
    {
        public Top10Agents(IReadOnlyCollection<RealEstateAgent> agents)
        {
            Agents = agents;
        }

        public IReadOnlyCollection<RealEstateAgent> Agents { get; }
    }
}
