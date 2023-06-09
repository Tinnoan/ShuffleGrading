using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuffleGrading.Grading
{
    internal class ChiSquaredTest : IGradingMetric
    {
        public double Grade(int[] deck, bool[] origins)
        {
            // Number of groups to break the deck into for the Chi-squared test
            int numGroups = 4;

            // The observed counts are the counts of each card in each position within a group
            int[][] observed = new int[numGroups][];
            for (int i = 0; i < numGroups; i++)
            {
                observed[i] = new int[deck.Length / numGroups + (i < deck.Length % numGroups ? 1 : 0)];
            }

            int groupSize = deck.Length / numGroups;
            int extraCards = deck.Length % numGroups;

            for (int i = 0; i < deck.Length; i++)
            {
                int group;
                int position;
                if (i < (groupSize + 1) * extraCards)
                {
                    group = i / (groupSize + 1);
                    position = i % (groupSize + 1);
                }
                else
                {
                    group = (i - extraCards) / groupSize;
                    position = (i - extraCards) % groupSize;
                }
                observed[group][position]++;
            }

            // The expected counts are the counts of each card in each position within a group in a perfectly ordered deck
            double[] expected = new double[groupSize + (extraCards > 0 ? 1 : 0)];
            for (int i = 0; i < expected.Length; i++)
            {
                expected[i] = numGroups;
            }

            // Calculate the chi-squared statistic
            double chi = 0.0;
            for (int i = 0; i < numGroups; i++)
            {
                chi += ChiSqTest(observed[i], expected);
            }

            return chi;
        }

        public double ChiSqTest(int[] observed, double[] expected)
        {
            double sum = 0.0;
            for (int i = 0; i < observed.Length; i++)
            {
                double diff = observed[i] - expected[i];
                sum += (diff * diff) / expected[i];
            }
            return sum;
        }
    }
}
