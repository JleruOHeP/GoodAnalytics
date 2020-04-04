using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCalculator.Models
{
    public interface IProcessElement
    {
        int? NextElementId { get; }
        void Execute(StateModel state);        
    }
}
