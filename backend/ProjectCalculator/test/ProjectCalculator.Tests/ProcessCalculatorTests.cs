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
                        Actions = new List<string> { "x = 10", "y = 5" },
                        NextElementId = 2                        
                    },
                    new ElementModel 
                    {
                        Id = 2,
                        ElementType = ElementType.Block,
                        Actions = new List<string> { "x = x + y" },
                        NextElementId = 3                        
                    },
                    new ElementModel 
                    {
                        Id = 3,
                        ElementType = ElementType.Block,
                        Actions = new List<string> { "y = a" }
                    }
                }
            };

            var finalState = calculator.Calculate(process);
            
            Assert.Equal(15, finalState.Variables["x"]);
            Assert.Equal(0, finalState.Variables["y"]);
            Assert.Equal(0, finalState.Variables["a"]);
        }
    }
}
