using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ProjectCalculator.Models;

namespace ProjectCalculator.Helpers
{
    public static class ConditionElementInitializer
    {
        //Expects input string in a format of "Condition:IdOfNextNode"
        //Condition can be a state check logical condition
        public static Func<StateModel, int?> GetDecisions(string inputEquation)
        {
            var cleanInput = inputEquation.Replace(" ", ""); // no spaces
            
            var iputParts = cleanInput.Split(":");

            var conditionInput = iputParts[0];
            var nextIdInput = iputParts[1];
            
            return (StateModel state) => 
            {
                int nextId;
                if (ParseCondition(conditionInput)(state) &&
                    int.TryParse(nextIdInput, out nextId))
                    return nextId;

                return null;
            };
        }
        
        private static Func<StateModel, bool> ParseCondition(string input)
        {
            if (input.Contains(">"))
            {
                return ParseGreaterCheck(input);
            }
            else if (input.Contains("<"))
            {
                return ParseLessCheck(input);
            }
            else if (input.Contains("="))
            {
                return ParseEqualityCheck(input);
            }
            else
            {
                //default - always true
                return (StateModel state) => true;
            }
        }
        
        private static Func<StateModel, bool> ParseGreaterCheck(string input)
        {
            return (StateModel state) => 
            {
                var operands = TryParseOperands(input.Split(">"), state);
                return operands.a > operands.b;
            };
        }

        private static Func<StateModel, bool> ParseLessCheck(string input)
        {
            return (StateModel state) => 
            {
                var operands = TryParseOperands(input.Split("<"), state);
                return operands.a < operands.b;
            };
        }

        private static Func<StateModel, bool> ParseEqualityCheck(string input)
        {
            return (StateModel state) => 
            {
                var operands = TryParseOperands(input.Split("="), state);
                return operands.a == operands.b;
            };
        }
    
        private static (decimal a, decimal b) TryParseOperands(string[] operands, StateModel state)
        {
            decimal a;
            decimal b;

            if (!decimal.TryParse(operands[0], out a)) //a is a reference!                
                a = state.GetVariable(operands[0]);
            
            if (!decimal.TryParse(operands[1], out b)) //b is a reference!
                b =  state.GetVariable(operands[1]);

            return (a: a, b: b);
        }
    }
}
