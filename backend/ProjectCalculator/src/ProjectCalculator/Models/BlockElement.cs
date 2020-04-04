using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCalculator.Models
{
    public class BlockElement: IProcessElement
    {        
        public void Execute()
        {

        }
        public IProcessElement MoveNext()
        {
            return null;
        }
    }
}
