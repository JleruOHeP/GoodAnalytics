using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ProjectCalculator.Helpers;

namespace ProjectCalculator.Models
{
    public class ConditionElement: IProcessElement
    {
        public int? NextElementId {get; private set;}

        public ConditionElement(List<string> functions)
        {
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
                    NextElementId = possibleNextElementId;
                    break;
                }                    
            }            
        }
    }
}
