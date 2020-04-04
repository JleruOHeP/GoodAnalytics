using System;

namespace ProjectCalculator.Models
{
    public enum ElementType
    {        
        Block, //simple action
        Condition, //predefined condition based on state
        Event //probability-based condition
    }
}
