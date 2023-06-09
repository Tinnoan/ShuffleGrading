using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuffleGrading.Grading
{
    public class Entropy : IGradingMetric
    {
        private int length;

        public Entropy(int length)
        {
            this.length = length;
        }

        public double Grade(int[] deck, bool[] origins)
        {
            List<int[]> permutations = PermutationEntropy.GeneratePermutations(deck, length);
            PermutationEntropy.CountPermutations(permutations);
            double entropy = PermutationEntropy.CalculateEntropy();
            return entropy;
        }
    }
}
