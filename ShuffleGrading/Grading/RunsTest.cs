using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuffleGrading.Grading
{
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
