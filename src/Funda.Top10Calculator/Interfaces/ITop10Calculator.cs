using System.Collections.Generic;
using Funda.Domain;
using Funda.Top10Calculator.Entities;

namespace Funda.Top10Calculator.Interfaces
{
    public interface ITop10Calculator
    {
        Top10Agents CalculateTop10Agents(IReadOnlyCollection<RealEstate> objects);
    }
}
