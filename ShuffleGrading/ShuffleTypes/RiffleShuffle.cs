namespace ShuffleGrading.ShuffleTypes
{
    /// <summary>
    /// This is a riffle shuffle. It splits the deck into two halves, then randomly interleaves the cards from the two halves.
    /// </summary>
    public class RiffleShuffle : IShuffle
    {
        private static readonly Random Rng = new();

        public string? Name { get; } = "Riffle Shuffle";

        public void Shuffle(int[] deck, bool[] origins)
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
    }
}
