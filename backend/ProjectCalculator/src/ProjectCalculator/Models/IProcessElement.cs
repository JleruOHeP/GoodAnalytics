using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCalculator.Models
{
    public interface IProcessElement
    {
        List<int> PreviousElementIds { get; }
        List<int> NextElementIds { get; }
        void Execute(StateModel state);
        bool IsExecuted { get; set; }
    }
}
