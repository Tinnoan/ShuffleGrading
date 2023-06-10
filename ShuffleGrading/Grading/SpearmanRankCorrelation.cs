using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuffleGrading.Grading
{
    internal class SpearmanRankCorrelation : IGradingMetric
    {
        /// <summary>
        /// This is a Spearman Rank Correlation test. It is a non-parametric measure of the monotonicity of the relationship between two data sets.
        /// </summary>
        public SpearmanRankCorrelation()
        {
            
        }
        public string? Name { get; } = "Spearman Rank Correlation";
        public double Grade(int[] deck, bool[] origins, int[] originalDeck)
        {
            // Check if deck and originalDeck have the same length
            if (deck.Length != originalDeck.Length)
            {
                throw new ArgumentException("Deck and originalDeck must be of the same length");
            }

            // Get the ranks
            var rankDeck = GetRanks(deck);
            var rankOriginalDeck = GetRanks(originalDeck);

            double dSquareSum = 0;
            int n = deck.Length;

            for (int i = 0; i < n; i++)
            {
                var d = rankDeck[i] - rankOriginalDeck[i];
                dSquareSum += d * d;
            }

            return 1 - (6 * dSquareSum) / ((double)n * (n * n - 1));
        }

        private int[] GetRanks(int[] data)
        {
            var sortedData = data
                .Select((x, i) => new KeyValuePair<int, int>(x, i))
                .OrderBy(x => x.Key)
                .ToArray();

            var ranks = new int[data.Length];

            for (int i = 0; i < ranks.Length; i++)
            {
                ranks[sortedData[i].Value] = i + 1;
            }

            return ranks;
        }
    }
}
