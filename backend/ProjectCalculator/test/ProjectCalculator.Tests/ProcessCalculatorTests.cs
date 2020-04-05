using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;

using ProjectCalculator.Calculators;
using ProjectCalculator.Models;

namespace ProjectCalculator.Tests
{
    public class ProcessCalculatorTests
    {
        [Fact]
        public void TestToUpperFunction()
        {
            var calculator = new ProcessCalculator();
            Assert.NotNull(calculator);

            var process = new ProcessModel 
            {
                FirstElementId = 1,
                Elements = new List<ElementModel>
                {
                    new ElementModel 
                    {
                        Id = 1,
                        ElementType = ElementType.Block,
                        Actions = new List<string> { "x = 10" },
                        NextElementId = 2                        
                    },
                    new ElementModel 
                    {
                        Id = 2,
                        ElementType = ElementType.Condition,
                        Actions = new List<string> { 
                            "x = 10 : 3", 
                            "x < 10 : 4", 
                            "x > 10 : 0" 
                        }
                    },
                    new ElementModel //if x = 10 - this will be executed!
                    {
                        Id = 3,
                        ElementType = ElementType.Block,
                        Actions = new List<string> { "y = x + 10" }
                    },
                    new ElementModel //if x < 10 - this will not be executed!
                    {
                        Id = 4,
                        ElementType = ElementType.Block,
                        Actions = new List<string> { "y = a + x" } //a is 0!
                    }
                }
            };

            var finalState = calculator.Calculate(process);
            
            Assert.Equal(10, finalState.Variables["x"]);
            Assert.Equal(20, finalState.Variables["y"]);
            Assert.Equal(false, finalState.Variables.ContainsKey("a"));
        }
    }
}
