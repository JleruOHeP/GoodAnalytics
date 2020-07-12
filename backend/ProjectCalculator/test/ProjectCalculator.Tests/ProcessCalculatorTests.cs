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
        public void LinearProcessTest()
        {
            var calculator = new ProcessCalculator();
            Assert.NotNull(calculator);

            var process = new InputProcessModel 
            {
                Elements = new List<InputElementModel>
                {
                    new InputElementModel 
                    {
                        Id = 1,
                        ElementType = ElementType.Block,
                        Actions = new List<string> { "x = 10" },
                        NextElementIds = new List<int>() {2}
                    },
                    new InputElementModel 
                    {
                        Id = 2,
                        ElementType = ElementType.Condition,
                        Actions = new List<string> { 
                            "x = 10 : 3", 
                            "x < 10 : 4", 
                            "x > 10 : 0" 
                        },
                        PreviousElementIds = new List<int>() {1}
                    },
                    new InputElementModel //if x = 10 - this will be executed!
                    {
                        Id = 3,
                        ElementType = ElementType.Block,
                        Actions = new List<string> { "y = x + 10" },
                        PreviousElementIds = new List<int>() {2}
                    },
                    new InputElementModel //if x < 10 - this will not be executed!
                    {
                        Id = 4,
                        ElementType = ElementType.Block,
                        Actions = new List<string> { "y = a + x" }, //a is 0!
                        PreviousElementIds = new List<int>() {2}
                    }
                }
            };

            var finalState = calculator.Calculate(process);
            
            Assert.Equal(10, finalState.Variables["x"]);
            Assert.Equal(20, finalState.Variables["y"]);
            Assert.Equal(false, finalState.Variables.ContainsKey("a"));
        }

        [Fact]
        public void XShapedProcessTest()
        {
            //  1 --- \   / 5 - 7
            //         4 -
            //  2 - 3 /   \ 6 - 8
            //5 - impossible event; 6 - certain event
            //4 - uses input of both 1 & 2
            //the rest - simple initialization actions
            
            var calculator = new ProcessCalculator();
            Assert.NotNull(calculator);

            var process = new InputProcessModel 
            {
                Elements = new List<InputElementModel>
                {
                    new InputElementModel 
                    {
                        Id = 1,
                        ElementType = ElementType.Block,
                        Actions = new List<string> { "x = 10" },
                        NextElementIds = new List<int>() {4}
                    },
                    new InputElementModel 
                    {
                        Id = 2,
                        ElementType = ElementType.Block,
                        Actions = new List<string> { "y = 20" },
                        NextElementIds = new List<int>() {3}
                    },
                    new InputElementModel 
                    {
                        Id = 3,
                        ElementType = ElementType.Block,
                        Actions = new List<string> { "z = 30" },
                        NextElementIds = new List<int>() {4},
                        PreviousElementIds = new List<int>() {2}
                    },
                    new InputElementModel 
                    {
                        Id = 4,
                        ElementType = ElementType.Block,
                        Actions = new List<string> { "xy = x + y" },
                        NextElementIds = new List<int>() {5, 6},
                        PreviousElementIds = new List<int>() {1, 3}
                    },
                    new InputElementModel 
                    {
                        Id = 7,
                        ElementType = ElementType.Block,
                        Actions = new List<string> { "a = 100" },
                        PreviousElementIds = new List<int>() {5}
                    },
                    new InputElementModel 
                    {
                        Id = 8,
                        ElementType = ElementType.Block,
                        Actions = new List<string> { "b = 200" },
                        PreviousElementIds = new List<int>() {6}
                    },
                    new InputElementModel 
                    {
                        Id = 5,
                        ElementType = ElementType.Event,
                        Actions = new List<string> { "100%" },
                        NextElementIds = new List<int>() {7},
                        PreviousElementIds = new List<int>() {4}
                    },
                    new InputElementModel 
                    {
                        Id = 6,
                        ElementType = ElementType.Event,
                        Actions = new List<string> { "0%" },
                        NextElementIds = new List<int>() {8},
                        PreviousElementIds = new List<int>() {4}
                    },
                }
            };

            var finalState = calculator.Calculate(process);
            
            Assert.Equal(30, finalState.Variables["xy"]);
            Assert.Equal(100, finalState.Variables["a"]);
            Assert.Equal(false, finalState.Variables.ContainsKey("b"));
        }
    }
}
