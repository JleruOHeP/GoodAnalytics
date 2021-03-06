using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using CustomerSpreadCalculator.Calculators;
using CustomerSpreadCalculator.Models;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace CustomerSpreadCalculator
{
    public class Function
    {
        
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public object FunctionHandler(InputModel input, ILambdaContext context)
        {
            if (input == null)
            {
                return new List<int> {42};
            }

            if (input.WorkStart > input.BusiestHour || input.BusiestHour > input.WorkEnd)
            {
                return new List<int>();
            }

            var weightCalculator = new WeightCalculator();
            var spreadCalculator = new SpreadCalculator();

            var weights = weightCalculator.Calculate(input);
            var spread = spreadCalculator.Calculate(input, weights);

            return spread;
        }
    }
}
