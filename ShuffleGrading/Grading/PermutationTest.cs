using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuffleGrading.Grading
{
    public class PermutationTest : IGradingMetric
    {
        private static Random random = new Random();
        private int numPermutations = 10000;
        /// <summary>
        /// This is a permutation test. It is a type of non-parametric method used to test the null hypothesis that two samples come from the same distribution.
        /// It tests whether the observed difference between groups is due to chance.
        /// </summary>
        public PermutationTest()
        {
            
        }
        public string? Name { get; } = "Permutation Test";
        public double Grade(int[] deck, bool[] origins, int[] originalDeck)
        {
            int observedDifference = CountDifferences(deck);

            int countGreater = 0;
            for (int i = 0; i < numPermutations; i++)
            {
                int[] permutedDeck = Permute(deck);
                int permutedDifference = CountDifferences(permutedDeck);
                if (permutedDifference >= observedDifference)
                {
                    countGreater++;
                }
            }

            double pValue = (double)countGreater / numPermutations;
            return pValue;
        }

        private int CountDifferences(int[] deck)
        {
            int count = 0;
            for (int i = 0; i < deck.Length; i++)
            {
                if (deck[i] != i + 1) // Notice the change here
                {
                    count++;
                }
            }
            return count;
        }

        private int[] Permute(int[] array)
        {
            int[] copy = (int[])array.Clone();
            for (int i = copy.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (copy[i], copy[j]) = (copy[j], copy[i]);
            }
            return copy;
        }
    }
}
