using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuffleGrading.ShuffleTypes
{
    public class Overhand : IShuffle
    {
        /// <summary>
        /// This is a simple overhand shuffle. It takes a random slice from the top of the deck and appends it to the bottom.
        /// </summary>
        public Overhand()
        {
            
        }
        private readonly Random _random = new();
        public string? Name => "Overhand Shuffle";

        public void Shuffle(int[] deck, bool[]? origins)
        {
            Random random = new Random();
            int n = deck.Length;
            var shuffledDeck = new List<int>();

            int[] deckCopy = new int[n];
            Array.Copy(deck, deckCopy, n);

            while (deckCopy.Length > 0)
            {
                // Decide the size of the next "slice" - you can experiment with different slice sizes
                int sliceSize = random.Next(1, Math.Min(deckCopy.Length, 10));

                // Take a slice from the deck
                var slice = deckCopy.Take(sliceSize).ToList();
                deckCopy = deckCopy.Skip(sliceSize).ToArray();

                // Append the slice to the shuffled deck
                shuffledDeck.InsertRange(0, slice);
            }

            // Copy the shuffled deck back into the original deck
            Array.Copy(shuffledDeck.ToArray(), deck, n);
        }
    }
}
