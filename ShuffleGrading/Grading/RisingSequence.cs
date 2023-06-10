using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuffleGrading.Grading
{
    public class RisingSequence : IGradingMetric
    {
        /// <summary>
        /// This is a rising sequence test. It counts the number of rising sequences in the deck.
        /// A rising sequence is a sequence of cards where each card is greater than the previous card.
        /// </summary>
        public RisingSequence()
        {
            
        }
        public string? Name { get; } = "Rising Sequence";

        public double Grade(int[] deck, bool[] origins, int[] originalDeck)
        {
            int risingSequences = 1;  // The first card starts a sequence
            for (int i = 1; i < deck.Length; i++)
            {
                // If the current card is less than or equal to the previous one, it's the start of a new rising sequence.
                if (deck[i] <= deck[i - 1])
                {
                    risingSequences++;
                }
            }
            // Return the number of rising sequences. Lower is better.
            return risingSequences;
        }
    }
}
