namespace ShuffleGrading.ShuffleTypes
{
    /// <summary>
    /// This is a perfect riffle shuffle. It splits the deck into two halves, then interleaves the cards from the two halves.
    /// </summary>
    internal class PerfectRiffleShuffle : IShuffle
    {
        public string Name { get; } = "Perfect Riffle Shuffle";

        public void Shuffle(int[] deck, bool[] origins)
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
    }
}
