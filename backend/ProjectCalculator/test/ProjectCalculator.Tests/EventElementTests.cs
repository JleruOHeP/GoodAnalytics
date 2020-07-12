using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;

using ProjectCalculator.Models;

namespace ProjectCalculator.Tests
{
    public class EventElementTest
    {
        [Fact]
        public void InitializationForCertainProbabilityTest()
        {
            var input = new List<string> {
                "101%"
            };
            var element = new EventElement(null, new List<int>() { 1 }, input);

            var state = new StateModel {
                Variables = new Dictionary<string, decimal> { }
            };
                        
            element.Execute(state);            
            Assert.Contains(1, element.NextElementIds);
        }

        [Fact]
        public void InitializationForImpossibleProbabilityTest()
        {
            var input = new List<string> {
                "0%"
            };
            var element = new EventElement(null, new List<int>() { 1 }, input);

            var state = new StateModel {
                Variables = new Dictionary<string, decimal> { }
            };
                        
            element.Execute(state);            
            Assert.Equal(0, element.NextElementIds.Count);
        }
    }
}
