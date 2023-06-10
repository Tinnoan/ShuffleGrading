using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuffleGrading.Grading
{
    public class ChiSquaredTest : IGradingMetric
    {
        /// <summary>
        /// This is a chi squared test. It calculates the chi squared statistic of the deck.
        /// The chi squared statistic is calculated by dividing the deck into 4 sections and counting the number of cards in the top half of each section.
        /// </summary>
        public ChiSquaredTest()
        {
            
        }
        public string? Name { get; } = "Chi Squared Test";

        public double Grade(int[] deck, bool[] origins)
        {
            int numSections = 4;
            int sectionSize = deck.Length / numSections;
            double expectedCount = sectionSize / 2.0;

            double chiSquareStatistic = 0;

            for (int i = 0; i < numSections; i++)
            {
                int start = i * sectionSize;
                int end = start + sectionSize;
                int topHalfCount = origins.Skip(start).Take(sectionSize).Count(origin => origin);
                int bottomHalfCount = sectionSize - topHalfCount;

                chiSquareStatistic += Math.Pow(topHalfCount - expectedCount, 2) / expectedCount;
                chiSquareStatistic += Math.Pow(bottomHalfCount - expectedCount, 2) / expectedCount;
            }

            return chiSquareStatistic;
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
