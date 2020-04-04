using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ProjectCalculator.Models;

namespace ProjectCalculator.Calculators
{
    public class ProcessCalculator
    {        
        public StateModel Calculate(ProcessModel inputModel)
        {
            //1 - generate a dictionary of elements
            var elements = GetProcessElements(inputModel);

            //initially state is empty
            var state = new StateModel();
            var currentElement = elements[inputModel.FirstElementId];

            while (currentElement != null)
            {
                currentElement.Execute(state);
                currentElement = currentElement.NextElementId != null 
                    ? elements[currentElement.NextElementId.Value] 
                    : null;
            }

            return state;
        }

        private Dictionary<int, IProcessElement> GetProcessElements(ProcessModel inputModel)
        {
            var elements = new Dictionary<int, IProcessElement>();
            foreach (var processModel in inputModel.Elements)
            {
                IProcessElement element = null;
                switch (processModel.ElementType)
                {
                    case ElementType.Block:
                        element = new BlockElement(processModel.NextElementId, processModel.Actions);
                        break;
                }

                elements[processModel.Id] = element; 
            }
            return elements;
        }
    }
}
