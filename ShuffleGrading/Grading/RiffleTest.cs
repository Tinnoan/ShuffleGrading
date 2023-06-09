using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuffleGrading.Grading
{
    public class RiffleTest : IGradingMetric
    {
        public double Grade(int[] deck, bool[] origins)
        {
            int switches = 0;
            for (int i = 1; i < deck.Length; i++)
            {
                // If the current card's origin is different from the previous card's origin, it's a switch.
                if (origins[i] != origins[i - 1])
                {
                    switches++;
                }
            }
            // Return the number of switches. Higher is better.
            return switches;
        }
    }
}
