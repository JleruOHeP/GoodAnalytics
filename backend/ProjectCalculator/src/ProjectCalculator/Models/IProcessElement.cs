using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCalculator.Models
{
    public interface IProcessElement
    {        
        void Execute();
        IProcessElement MoveNext();
    }
}
