using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ProjectCalculator.Helpers;

namespace ProjectCalculator.Models
{
    public class EventElement: IProcessElement
    {
        public List<int> NextElementIds {get; private set;}
        public List<int> PreviousElementIds { get; private set; }
        public bool IsExecuted { get; set; }

        public EventElement(List<int> previousElementIds, List<int> nextElementIds, List<string> functions)
        {
            NextElementIds = nextElementIds != null ? new List<int>(nextElementIds) : new List<int>();
            PreviousElementIds = previousElementIds != null ? new List<int>(previousElementIds) : new List<int>();

            Probabilities = new List<Func<bool>>();
            foreach (var input in functions)
            {
                Probabilities.Add(EventElementInitializer.GetProbability(input));
            }
        }

        private List<Func<bool>> Probabilities;

        public void Execute(StateModel state)
        {
            foreach (var probabilityCheck in Probabilities)
            {
                if (!probabilityCheck()){
                    NextElementIds.Clear();
                    break;
                }
            }
            IsExecuted = true;
        }
    }
}
