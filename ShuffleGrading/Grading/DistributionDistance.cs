using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuffleGrading.Grading
{
    public class DistributionDistance : IGradingMetric
    {
        /// <summary>
        /// This is a distribution of distances between card positions test. It calculates the distribution of distances between card positions.
        /// </summary>
        public DistributionDistance()
        {
            
        }
        public string? Name { get; } = "Distribution distance";

        public double Grade(int[] deck, bool[] origins)
        {
            int[] originalDeck = Enumerable.Range(0, deck.Length).ToArray();
            int[] distances = new int[deck.Length];
            for (int i = 0; i < deck.Length; i++)
            {
                int originalPosition = Array.IndexOf(originalDeck, deck[i]);
                int shuffledPosition = i;
                distances[i] = Math.Abs(originalPosition - shuffledPosition);
            }

            // Now distances contains the absolute differences between original and shuffled positions
            // for each card. You can analyze this array as needed, for example by calculating its mean,
            // standard deviation, etc.

            // Here we simply sum the distances as a basic measure of the shuffle's "randomness".
            // We convert it to a double to satisfy the IGradingMetric interface.
            double totalDistance = (double)distances.Sum() / (deck.Length * (deck.Length - 1) / 2);

            return totalDistance;
        }

    }
}
