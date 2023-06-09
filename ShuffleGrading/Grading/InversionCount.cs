using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuffleGrading.Grading
{
    internal class InversionCount : IGradingMetric
    {
        public double Grade(int[] deck, bool[] origins)
        {
            int inversions = 0;
            for (int i = 0; i < deck.Length; i++)
            {
                for (int j = i + 1; j < deck.Length; j++)
                {
                    if (deck[i] > deck[j])
                    {
                        inversions++;
                    }
                }
            }
            return inversions;
        }
    }
}
