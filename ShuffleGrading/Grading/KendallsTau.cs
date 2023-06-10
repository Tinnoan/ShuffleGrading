using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuffleGrading.Grading
{
    internal class KendallsTau : IGradingMetric
    {
        /// <summary>
        /// This is a Kendall's Tau test. It counts the number of concordant and discordant pairs in the deck.
        /// It does this by making pairwise comparisons between each card in the deck.
        /// </summary>
        public KendallsTau()
        {
            
        }
        public string? Name { get; } = "Kendall's Tau";
        public double Grade(int[] deck, bool[] origins, int[] originalDeck)
        {
            int n = deck.Length;
            int concordant = 0;
            int discordant = 0;

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    int originalI = Array.IndexOf(originalDeck, deck[i]);
                    int originalJ = Array.IndexOf(originalDeck, deck[j]);

                    if ((originalI < originalJ && i < j) || (originalI > originalJ && i > j))
                    {
                        concordant++;
                    }
                    else
                    {
                        discordant++;
                    }
                }
            }

            return (concordant - discordant) / (double)(concordant + discordant);
        }
    }
}
