using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ProjectCalculator.Helpers;

namespace ProjectCalculator.Models
{
    public class ConditionElement: IProcessElement
    {
        public List<int> NextElementIds {get; private set;}
        public List<int> PreviousElementIds { get; private set; }
        public bool IsExecuted { get; set; }

        public ConditionElement(List<int> previousElementIds, List<string> functions)
        {
            NextElementIds = new List<int>();
            PreviousElementIds = previousElementIds != null ? new List<int>(previousElementIds) : new List<int>();

            Conditions = new List<Func<StateModel, int?>>();
            foreach (var input in functions)
            {
                Conditions.Add(ConditionElementInitializer.GetDecisions(input));
            }
        }

        private List<Func<StateModel, int?>> Conditions;

        public void Execute(StateModel state)
        {
            foreach (var conditionCheck in Conditions)
            {
                var possibleNextElementId = conditionCheck(state);
                if (possibleNextElementId != null){
                    NextElementIds.Add(possibleNextElementId.Value);
                    break;
                }                    
            }
            IsExecuted = true;
        }
    }
}
