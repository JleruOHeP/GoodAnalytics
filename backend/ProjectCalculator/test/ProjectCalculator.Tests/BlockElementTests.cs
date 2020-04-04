using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;

using ProjectCalculator.Models;

namespace ProjectCalculator.Tests
{
    public class BlockElementTests
    {
        [Fact]
        public void InitializationTest()
        {
            var input = new List<string> {
                "y = 145 + x",
                "y = y * 2",  

                "x = x / 5",

                "z = y",
                "z = z - 2",

                "a = b + 10"
            };
            var element = new BlockElement(0, input);

            var state = new StateModel {
                Variables = new Dictionary<string, decimal> {
                    {"static", 19},
                    {"x", 5},
                    {"z", 0}
                }
            };

            element.Execute(state);
            
            Assert.Equal(19, state.Variables["static"]);
            Assert.Equal(1, state.Variables["x"]); // x = x / 5
            Assert.Equal(300, state.Variables["y"]); // y = (145 + x[5]) * 2
            Assert.Equal(298, state.Variables["z"]);
            Assert.Equal(10, state.Variables["a"]);
            Assert.Equal(0, state.Variables["b"]);  //set during first access
        }
    }
}
