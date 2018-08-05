using System;
using System.Collections.Generic;
using CustomerSpreadCalculator.Models;

namespace CustomerSpreadCalculator.Calculators
{
    public class WeightCalculator
    {
        public List<double> Calculate(InputModel input)
        {
            var result = new List<double>();

            //assume that work starts at 0:
            var start = 0;
            var busiestHour = input.BusiestHour - input.WorkStart;
            var end = input.WorkEnd - input.WorkStart;

            //modeling weights as a linear function y = kx + b

            //calcutate up to a busiest hour:            
            //coefficients
            var k = (2 * input.AverageCustomersPerDay) / (busiestHour * end);            
            var b = 1.0;
            for (int i = 0; i <= busiestHour; i++)
            {
                var weight = k * i + b; // minimum weight is 0
                result.Add(weight);
            }

            //calculate from the busiest hour till the end
            //coefficients
            k = ( (2 * input.AverageCustomersPerDay) / end - 1 ) / (busiestHour - end);
            b = 1 - (2 * input.AverageCustomersPerDay - end) / (busiestHour - end);
            for (int i = busiestHour + 1; i <= end; i++)
            {
                var weight = k * i + b;
                result.Add(weight);
            }

            return result;
        }
    }
}