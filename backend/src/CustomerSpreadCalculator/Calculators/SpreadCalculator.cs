using System;
using System.Collections.Generic;
using System.Linq;
using CustomerSpreadCalculator.Models;

namespace CustomerSpreadCalculator.Calculators
{
    public class SpreadCalculator
    {
        public List<int> Calculate(InputModel input, List<double> hourlyWeights)
        {
            var result = new List<int>();

            //normalize the times - start time will be 0;
            var start = 0;
            var spike = input.BusiestHour - input.WorkStart;
            var end = input.WorkEnd - input.WorkStart;
            
            var random = new Random();
            for (double currentTime = start; currentTime < end; currentTime = currentTime + 0.5)
            {
                var probability = CalculateNormalDistribution(currentTime, spike);
                var randomValue = random.NextDouble();                
                //round down the current time to match the hourlyWeights distribution.
                var matchingWeightIndex = (int)currentTime;
                var customers = randomValue < probability ? (int)hourlyWeights[matchingWeightIndex] : 0;
                result.Add(customers);
            }

            return result;
        }

        public double CalculateNormalDistribution(double currentTime, int spikeTime)
        {
            var mean = spikeTime; //mu
            var variance = 1; //sigma^2

            var probability = 1 / (Math.Sqrt(2 * Math.PI * variance))
                              * 
                              Math.Pow(Math.E, - ((currentTime - mean) * (currentTime - mean)) / (2 * variance));

            return probability + 0.7 - 0.09894228040143;
        }
    }
}