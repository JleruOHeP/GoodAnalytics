using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;

using ProjectCalculator.Models;

namespace ProjectCalculator.Tests
{
    public class ConditionElementTest
    {
        [Fact]
        public void InitializationForStateCheckTest()
        {
            var input = new List<string> {
                "x<10:1",
                "5 > y : 2",
                "rubbish:3"
            };
            var element = new ConditionElement(null, input);

            var state = new StateModel {
                Variables = new Dictionary<string, decimal> { 
                    {"x", 100},
                    {"y", 100}
                }
            };
            
            //first check - no state - rubbish should fire as a default route
            element.Execute(state);            
            Assert.Contains(3, element.NextElementIds);

            //second check - no conditions met from state - rubbish should fire as a default route
            state.Variables["y"] = 10; 
            element.Execute(state);            
            Assert.Contains(3, element.NextElementIds);

            //third check - second condition from state is true
            state.Variables["y"] = 2; 
            element.Execute(state);            
            Assert.Contains(2, element.NextElementIds);

            //fourth check - first condition from state is true
            state.Variables["x"] = 1; 
            element.Execute(state);            
            Assert.Contains(1, element.NextElementIds);
        }
    }
}
