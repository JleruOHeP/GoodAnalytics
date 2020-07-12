using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

using ProjectCalculator.Models;

using ProjectCalculator;

namespace ProjectCalculator.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void FunctionHandlerTest()
        {
            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();
            var process = new InputProcessModel 
            {
                Elements = new List<InputElementModel>
                {
                    new InputElementModel 
                    {
                        Id = 1,
                        ElementType = ElementType.Block,
                        Actions = new List<string> { "x = 10" }         
                    }
                }
            };
            var state = function.FunctionHandler(process, context);

            Assert.Equal(10, state.Variables["x"]);
        }
    }
}
