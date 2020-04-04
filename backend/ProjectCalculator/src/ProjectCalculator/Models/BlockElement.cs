using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ProjectCalculator.Helpers;

namespace ProjectCalculator.Models
{
    public class BlockElement: IProcessElement
    {
        public int? NextElementId {get; private set;}
        public BlockElement(int? nextElementId, List<string> functions)
        {
            NextElementId = nextElementId;

            Functions = new List<Action<StateModel>>();
            foreach (var input in functions)
            {
                Functions.Add(BlockElementInitializer.GetAction(input));
            }
        }

        private List<Action<StateModel>> Functions;

        public void Execute(StateModel state)
        {
            foreach (var function in Functions)
            {
                function(state);
            }
        }
    }
}
