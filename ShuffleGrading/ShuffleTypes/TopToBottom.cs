using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuffleGrading.ShuffleTypes
{
    /// <summary>
    /// This is a top-to-bottom shuffle. It moves the top card to the bottom of the deck. It is meant as an example of a bad shuffle.
    /// </summary>
    public class TopToBottom : IShuffle
    {
        public string? Name { get; } = "Top-to-bottom shuffle";

        public void Shuffle(int[] deck, bool[]? origins)
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
    }
}
