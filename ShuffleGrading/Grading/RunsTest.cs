using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuffleGrading.Grading
{
    /// <summary>
    /// This is a runs test. It counts the number of runs in the deck.
    /// A run is a sequence of cards where each card is one higher or one lower than the previous card.
    /// </summary>
    public class RunsTest : IGradingMetric
    {
        public double Grade(int[] deck, bool[] origins)
        {
            int runs = 0;
            for (int i = 0; i < deck.Length - 1; i++)
            {
                if (Math.Abs(deck[i + 1] - deck[i]) == 1 &&
                    (i == 0 || Math.Abs(deck[i] - deck[i - 1]) != Math.Abs(deck[i + 1] - deck[i])))
                {
                    runs++;
                }
            }
            return runs;
        }
    }
}
