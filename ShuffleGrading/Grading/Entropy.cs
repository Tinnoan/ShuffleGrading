using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShuffleGrading.Grading.Helpers;

namespace ShuffleGrading.Grading
{
    public class Entropy : IGradingMetric
    {
        private int length;

        /// <summary>
        /// This is a entropy test. It calculates the entropy of the deck.
        /// The entropy is calculated by counting the number of times each permutation appears in the deck.
        /// </summary>
        public Entropy(int length)
        {
            this.length = length;
        }

        public string? Name { get; } = "Entropy";

        public double Grade(int[] deck, bool[] origins)
        {
            List<int[]> permutations = GeneratePermutations(deck, length);
            CountPermutations(permutations);
            double entropy = CalculateEntropy();
            return entropy;
        }

        public static Dictionary<string, int> PermutationCounts = new();

        public static List<int[]> GeneratePermutations(int[] deck, int length)
        {
            List<int[]> permutations = new List<int[]>();
            Permute(deck, 0, length, permutations);
            return permutations;
        }

        private static void Permute(int[] deck, int start, int length, List<int[]> permutations)
        {
            if (length == 1)
            {
                int[] permutation = new int[start + 1];
                Array.Copy(deck, permutation, start + 1);
                permutations.Add(permutation);
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    Swap(deck, start, start + i);
                    Permute(deck, start + 1, length - 1, permutations);
                    Swap(deck, start, start + i); // backtrack
                }
            }
        }

        private static void Swap(int[] deck, int indexA, int indexB)
        {
            int tmp = deck[indexA];
            deck[indexA] = deck[indexB];
            deck[indexB] = tmp;
        }


        public static Dictionary<string, int> CountPermutations(List<int[]> permutations)
        {
            //var permutationCounts = new Dictionary<string, int>();

            foreach (var permutation in permutations)
            {
                // Convert the permutation to a string so it can be used as a dictionary key.
                var permutationString = string.Join(",", permutation);

                if (PermutationCounts.ContainsKey(permutationString))
                {
                    // If the permutation already exists in the dictionary, increment the count.
                    PermutationCounts[permutationString]++;
                }
                else
                {
                    // If the permutation does not exist in the dictionary, add it with a count of 1.
                    PermutationCounts.Add(permutationString, 1);
                }
            }

            return PermutationCounts;
        }


        public static double CalculateEntropy()
        {
            // Calculate the total number of permutations.
            int totalPermutations = PermutationCounts.Values.Sum();

            double entropy = 0;
            foreach (int count in PermutationCounts.Values)
            {
                // Calculate the probability of this permutation.
                double probability = (double)count / totalPermutations;

                // Add this permutation's contribution to the entropy.
                entropy -= probability * Math.Log(probability);
            }
            return entropy;
        }

        public static void ResetPermutationCounts()
        {
            PermutationCounts.Clear();
        }

        public static void PrintPermutationCounts()
        {
            foreach (KeyValuePair<string, int> permutationCount in PermutationCounts)
            {
                Console.WriteLine("{0}: {1}", permutationCount.Key, permutationCount.Value);
            }
        }
    }
}
