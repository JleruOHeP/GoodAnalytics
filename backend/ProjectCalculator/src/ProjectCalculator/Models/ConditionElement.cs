using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCalculator.Models
{
    public class ConditionElement: IProcessElement
    {
        public int? NextElementId {get; private set;}
        public void Execute(StateModel state)
        {
            NextElementId = null;
        }
    }
}
