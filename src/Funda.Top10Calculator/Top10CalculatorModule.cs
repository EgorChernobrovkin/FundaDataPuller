using Funda.Top10Calculator.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Funda.Top10Calculator
{
    public static class Top10CalculatorModule
    {
        public static void AddFundaTop10Calculator(this IServiceCollection services)
        {
            services.AddSingleton<ITop10Calculator, Impl.Top10Calculator>();
        }
    }
}
