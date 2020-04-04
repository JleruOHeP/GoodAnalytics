using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCalculator.Models
{
    public class StateModel
    {
        public StateModel()
        {
            Variables = new Dictionary<string, decimal>();
        }
        
        public decimal GetVariable(string name)
        {
            if (!Variables.ContainsKey(name))
                Variables[name] = 0;
            return Variables[name];
        }
        public Dictionary<string, decimal> Variables {get; set;}
        public string Message {get; set;}        
    }
}
