using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ProjectCalculator.Helpers;

namespace ProjectCalculator.Models
{
    public class BlockElement: IProcessElement
    {
        public List<int> NextElementIds { get; private set; }
        public List<int> PreviousElementIds { get; private set; }
        public bool IsExecuted { get; set; }

        public BlockElement(List<int> previousElementIds, List<int> nextElementIds, List<string> functions)
        {
            NextElementIds = nextElementIds != null ? new List<int>(nextElementIds) : new List<int>();
            PreviousElementIds = previousElementIds != null ? new List<int>(previousElementIds) : new List<int>();

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
            IsExecuted = true;
        }
    }
}
