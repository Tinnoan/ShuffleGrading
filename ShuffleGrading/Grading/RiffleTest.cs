using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuffleGrading.Grading
{
    /// <summary>
    /// This is a riffle test. It counts the number of switches in the deck.
    /// A switch is a pair of cards where the second card's origin is different from the first card's origin.
    /// </summary>
    public class RiffleTest : IGradingMetric
    {
        public string? Name { get; } = "Riffle Test";

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
