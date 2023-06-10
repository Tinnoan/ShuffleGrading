using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuffleGrading.Grading
{
    public class InversionCount : IGradingMetric
    {
        /// <summary>
        /// This is a inversion count test. It counts the number of inversions in the deck.
        /// An inversion is a pair of cards where the first card is higher than the second card.
        /// </summary>
        public InversionCount()
        {
            
        }
        public string? Name { get; } = "Inversion Count";

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
