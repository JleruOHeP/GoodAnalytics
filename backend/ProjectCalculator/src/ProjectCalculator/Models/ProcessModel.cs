using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCalculator.Models
{
    public class ProcessModel
    {
        public List<ElementModel> Elements {get; set;}
        public int FirstElementId {get; set;}
    }

    public class ElementModel
    {
        public int Id {get; set;}
        public ElementType ElementType {get; set;}
        public int? NextElementId {get; set;} // only for blocks!
        public List<string> Actions {get; set;}
    }
}
