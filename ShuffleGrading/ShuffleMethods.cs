using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuffleGrading
{
    public static class ShuffleMethods
    {
        private static readonly Random Rng = new();

        public static void PerfectRiffleShuffle(int[] deck, bool[] origins)
        {
            int n = deck.Length;
            int[] tempDeck = new int[n];
            bool[] tempOrigins = new bool[n];

            // Copy the original deck and origins.
            Array.Copy(deck, tempDeck, n);
            Array.Copy(origins, tempOrigins, n);

            int mid = n / 2;
            int index = 0;

            // Interleave cards from the two halves of the deck.
            for (int i = 0; i < mid; i++)
            {
                deck[index] = tempDeck[i];
                origins[index] = tempOrigins[i];
                index++;

                deck[index] = tempDeck[i + mid];
                origins[index] = tempOrigins[i + mid];
                index++;
            }

            // If the deck has an odd number of cards, add the last card from the second half.
            if (n % 2 != 0)
            {
                deck[index] = tempDeck[n - 1];
                origins[index] = tempOrigins[n - 1];
            }
        }

        public static void RiffleShuffle(int[] deck, bool[] origins)
        {
            int n = deck.Length;
            int[] tempDeck = new int[n];
            bool[] tempOrigins = new bool[n];

            // Copy the original deck and origins.
            Array.Copy(deck, tempDeck, n);
            Array.Copy(origins, tempOrigins, n);

            // Randomly decide where to cut the deck.
            int mid = Rng.Next(n);

            int index = 0;
            int topIndex = 0;
            int bottomIndex = mid;

            // Randomly interleave cards from the two halves of the deck.
            while (topIndex < mid && bottomIndex < n)
            {
                if (Rng.Next(2) == 0)
                {
                    deck[index] = tempDeck[topIndex];
                    origins[index] = tempOrigins[topIndex];
                    topIndex++;
                }
                else
                {
                    deck[index] = tempDeck[bottomIndex];
                    origins[index] = tempOrigins[bottomIndex];
                    bottomIndex++;
                }

                index++;
            }

            // If there are any cards left in the top half, add them.
            while (topIndex < mid)
            {
                deck[index] = tempDeck[topIndex];
                origins[index] = tempOrigins[topIndex];
                topIndex++;
                index++;
            }

            // If there are any cards left in the bottom half, add them.
            while (bottomIndex < n)
            {
                deck[index] = tempDeck[bottomIndex];
                origins[index] = tempOrigins[bottomIndex];
                bottomIndex++;
                index++;
            }
        }

        public static void TopToBottomShuffle(int[] deck, bool[] origins)
        {
            int topCard = deck[0];
            bool topOrigin = origins[0];

            // Shift all the cards and origins down one position.
            Array.Copy(deck, 1, deck, 0, deck.Length - 1);
            Array.Copy(origins, 1, origins, 0, origins.Length - 1);

            // Put the top card and origin on the bottom.
            deck[^1] = topCard;
            origins[^1] = topOrigin;
        }

        public static void PerfectShuffle(int[] deck, bool[] origins)
        {
            int n = deck.Length;
            while (n > 1)
            {
                n--;
                int k = Rng.Next(n + 1);

                // Swap the cards.
                (deck[k], deck[n]) = (deck[n], deck[k]);

                // Swap the origins.
                (origins[k], origins[n]) = (origins[n], origins[k]);
            }
        }

    }
}
