using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ProjectCalculator.Models;

namespace ProjectCalculator.Calculators
{
    public class ProcessCalculator
    {
        private  Dictionary<int, IProcessElement> ProcessElements;
        private StateModel ProcessState;

        public StateModel Calculate(InputProcessModel inputModel)
        {
            //1 - initialize calculator
            SetupProcessElements(inputModel);
            ProcessState = new StateModel();

            //2 - get starting elements
            var startingElements = ProcessElements.Values.Where(x => x.PreviousElementIds.Count == 0);

            foreach (var startingElement in startingElements)
            {
                ExecuteElement(startingElement);
            }

            return ProcessState;
        }

        private void ExecuteElement(IProcessElement element)
        {
            if (element.IsExecuted) return;
            element.IsExecuted = true; //do not want it here!!

            foreach (var parentId in element.PreviousElementIds)
            {
                ExecuteElement(ProcessElements[parentId]);
            }
            
            element.Execute(ProcessState);
            
            foreach (var nextId in element.NextElementIds)
            {
                ExecuteElement(ProcessElements[nextId]);
            }
        }

        private void SetupProcessElements(InputProcessModel inputModel)
        {
            ProcessElements = new Dictionary<int, IProcessElement>();
            foreach (var processModel in inputModel.Elements)
            {
                IProcessElement element = null;
                switch (processModel.ElementType)
                {
                    case ElementType.Block:
                        element = new BlockElement(processModel.PreviousElementIds, processModel.NextElementIds, processModel.Actions);
                        break;
                    case ElementType.Condition:
                        element = new ConditionElement(processModel.PreviousElementIds, processModel.Actions);
                        break;
                    case ElementType.Event:
                        element = new EventElement(processModel.PreviousElementIds, processModel.NextElementIds, processModel.Actions);
                        break;
                }

                ProcessElements[processModel.Id] = element; 
            }
        }
    }
}
