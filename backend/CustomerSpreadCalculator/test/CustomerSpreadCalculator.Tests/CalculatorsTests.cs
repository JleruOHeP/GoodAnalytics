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
        public void SpreadCalculateWithStaticWeightsTest()
        {
            var calculator = new SpreadCalculator();

            var input = new InputModel
            {                
                WorkStart = 9, 
                WorkEnd = 18,
                BusiestHour = 16
            };
            var weights = Enumerable.Repeat(1.0, 9).ToList();
            
            var customersForMonth = Enumerable.Repeat(0, 1000)
                .Select(x => calculator.Calculate(input, weights));

            //customers per hour. indicating number of customers from the interval start time till the next interval
            //9 - .5 - 10 - .5 - 11 - .5 - 12 - .5 - 13 - .5 - 14 - .5 - 15 - .5 - 16 - .5 - 17 - .5
            Assert.All(customersForMonth, (spread) => Assert.Equal(18, spread.Count));
                        
            var totalCustomersPerDay = customersForMonth.Select(spread => spread.Sum());

            Assert.True(Math.Abs(13 - totalCustomersPerDay.Average()) < 0.5);
        }

        [Fact]
        public void SpreadCalculateFullTest()
        {
            var calculator = new SpreadCalculator();

            var input = new InputModel
            {
                AverageCustomersPerDay = 30,
                WorkStart = 9, 
                WorkEnd = 18,
                BusiestHour = 16
            };
            var weights = new WeightCalculator().Calculate(input);
            
            var customersForMonth = Enumerable.Repeat(0, 1000)
                .Select(x => calculator.Calculate(input, weights));

            //customers per hour. indicating number of customers from the interval start time till the next interval
            //9 - .5 - 10 - .5 - 11 - .5 - 12 - .5 - 13 - .5 - 14 - .5 - 15 - .5 - 16 - .5 - 17 - .5
            Assert.All(customersForMonth, (spread) => Assert.Equal(18, spread.Count));
                        
            var totalCustomersPerDay = customersForMonth.Select(spread => spread.Sum());

            Assert.True(Math.Abs(50 - totalCustomersPerDay.Average()) < 1);
        }

        [Fact]
        public void ProbabilityCalculationTest()
        {
            var calculator = new SpreadCalculator();

            var maxProbability = calculator.CalculateNormalDistribution(7, 7);
            Assert.Equal(1.0, maxProbability, 1);
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

            //hours: 9 - 10 - 11 - 12 - 13 - 14 - 15 - 16 - 17 - 18
            //index: 0 - 1  - 2  - 3  - 4  - 5  - 6  - 7  - 8  - 9
            Assert.Equal(10, weights.Count);

            var busiestHourWeight = weights[7];
            Assert.Equal(busiestHourWeight, weights.Max());
            Assert.Equal(7.67, busiestHourWeight, 2);
            Assert.Equal(1, weights[0]);
            Assert.Equal(1, weights[9]);
        }
    }
}
