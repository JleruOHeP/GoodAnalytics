using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ProjectCalculator.Models;

namespace ProjectCalculator.Helpers
{
    public static class EventElementInitializer
    {
        //Expects input string in a format of "XX%"
        public static Func<bool> GetProbability(string input)
        {
            var probabilityInput = input.Replace(" ", ""); // no spaces
            
            if (probabilityInput.Contains("%"))
            {
                return () => 
                {
                    int probabilityPercentage;
                    if (!int.TryParse(probabilityInput.Replace("%", ""), out probabilityPercentage))
                        probabilityPercentage = 0;                

                    var random = new Random();
                    int randomValue = random.Next(0, 100);

                    return probabilityPercentage >= randomValue;
                };
            }
            else
            {
                //default - always true
                return () => true;
            }
        }
    }
}
