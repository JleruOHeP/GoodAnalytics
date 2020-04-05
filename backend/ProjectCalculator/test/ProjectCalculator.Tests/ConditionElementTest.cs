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
            var element = new ConditionElement(input);

            var state = new StateModel {
                Variables = new Dictionary<string, decimal> { 
                    {"x", 100},
                    {"y", 100}
                }
            };
            
            //first check - no state - rubbish should fire as a default route
            element.Execute(state);            
            Assert.Equal(3, element.NextElementId);

            //second check - no conditions met from state - rubbish should fire as a default route
            state.Variables["y"] = 10; 
            element.Execute(state);            
            Assert.Equal(3, element.NextElementId);

            //third check - second condition from state is true
            state.Variables["y"] = 2; 
            element.Execute(state);            
            Assert.Equal(2, element.NextElementId);

            //fourth check - first condition from state is true
            state.Variables["x"] = 1; 
            element.Execute(state);            
            Assert.Equal(1, element.NextElementId);
        }

        [Fact]
        public void InitializationForCertainProbabilityTest()
        {
            var input = new List<string> {
                "101%:1"
            };
            var element = new ConditionElement(input);

            var state = new StateModel {
                Variables = new Dictionary<string, decimal> { }
            };
                        
            element.Execute(state);            
            Assert.Equal(1, element.NextElementId);
        }

        [Fact]
        public void InitializationForImpossibleProbabilityTest()
        {
            var input = new List<string> {
                "0%:1"
            };
            var element = new ConditionElement(input);

            var state = new StateModel {
                Variables = new Dictionary<string, decimal> { }
            };
                        
            element.Execute(state);            
            Assert.Null(element.NextElementId);
        }
    }
}
