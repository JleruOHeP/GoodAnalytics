using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

using CustomerSpreadCalculator.Calculators;
using CustomerSpreadCalculator.Models;

namespace CustomerSpreadCalculator.Tests
{
    public class CalculatorsTests
    {
        [Fact]
        public void SpreadCalculateTest()
        {
            var calculator = new SpreadCalculator();

            var input = new InputModel
            {
                AverageCustomersPerDay = 30,
                WorkStart = 9, 
                WorkEnd = 18,
                BusiestHour = 16
            };
            var weights = Enumerable.Repeat(1.0, 9).ToList();
            var spread = calculator.Calculate(input, weights);
            Assert.Equal(18, spread.Count);

            foreach (var customersNumber in spread)
            {
                Console.WriteLine(customersNumber);
            }
        }

        [Fact]
        public void WeightCalculateTest()
        {
            var calculator = new WeightCalculator();

            var input = new InputModel
            {
                AverageCustomersPerDay = 30,
                WorkStart = 9, 
                WorkEnd = 18,
                BusiestHour = 16
            };
            var weights = calculator.Calculate(input);
            Assert.Equal(10, weights.Count);

            foreach (var weight in weights)
            {
                Console.Write(weight + " - ");
            }
        }
    }
}
