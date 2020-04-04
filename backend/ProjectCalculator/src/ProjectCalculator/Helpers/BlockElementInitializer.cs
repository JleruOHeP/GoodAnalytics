using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ProjectCalculator.Models;

namespace ProjectCalculator.Helpers
{
    public static class BlockElementInitializer
    {
        //Expects input string in a format of "x=y+z". It should be an assignemt and with only 1 operation
        public static Action<StateModel> GetAction(string inputEquation)
        {
            var cleanInput = inputEquation.Replace(" ", ""); // no spaces
            
            var iputParts = cleanInput.Split("=");

            var assignmentTarget = iputParts[0];
            var operation = iputParts[1];

            return (StateModel state) => 
            {
                state.Variables[assignmentTarget] = ParseOperations(operation)(state);
            };
        }
        
        private static Func<StateModel, decimal> ParseOperations(string input)
        {
            if (input.Contains("*"))
            {
                return ParseMultiplication(input);
            }
            else if (input.Contains("/"))
            {
                return ParseDivision(input);
            }
            else if (input.Contains("+"))
            {
                return ParseSummation(input);
            }
            else if (input.Contains("-"))
            {
                return ParseSubtraction(input);
            }
            else
            {
                return ParseAssignment(input);
            }
        }

        private static Func<StateModel, decimal> ParseMultiplication(string input)
        {
            return (StateModel state) => 
            {
                var operands = TryParseOperands(input.Split("*"), state);
                return operands.a * operands.b;
            };
        }

        private static Func<StateModel, decimal> ParseDivision(string input)
        {
            return (StateModel state) => 
            {
                var operands = TryParseOperands(input.Split("/"), state);
                return operands.a / operands.b;
            };
        }

        private static Func<StateModel, decimal> ParseSubtraction(string input)
        {
            return (StateModel state) => 
            {
                var operands = TryParseOperands(input.Split("-"), state);
                return operands.a - operands.b;
            };           
        }

        private static Func<StateModel, decimal> ParseSummation(string input)
        {
            return (StateModel state) => 
            {
                var operands = TryParseOperands(input.Split("+"), state);
                return operands.a + operands.b;
            };
        }

        private static Func<StateModel, decimal> ParseAssignment(string input)
        {            
            return (StateModel state) => 
            {
                decimal a;

                if (!decimal.TryParse(input, out a)) //a is a reference!
                    a = state.GetVariable(input);
                
                return a;
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
