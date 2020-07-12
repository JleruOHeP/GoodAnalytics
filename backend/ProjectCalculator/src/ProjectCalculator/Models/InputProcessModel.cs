using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCalculator.Models
{
    public class InputProcessModel
    {
        public List<InputElementModel> Elements {get; set;}
    }

    public class InputElementModel
    {
        public int Id {get; set;}
        public ElementType ElementType {get; set;}
        public List<int> NextElementIds {get; set;}
        public List<int> PreviousElementIds {get; set;}
        public List<string> Actions {get; set;}
    }
}
