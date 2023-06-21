using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuffleGrading.ShuffleTypes
{
    public class RandomPile : IShuffle
    {
        private readonly Random _random = new();
        private readonly int _piles;

        /// <summary>
        /// This is a random pile shuffle. It takes the deck and splits it into piles randomly, then puts the piles back together randomly.
        /// </summary>
        /// <param name="piles">This indicates the number of piles the pile shuffle will use.</param>
        public RandomPile(int piles)
        {
            _piles = piles;
            Name = $"random pile shuffle-{piles}";
        }
        public string? Name { get; }

        public void Shuffle(int[] deck, bool[]? origins)
        {
            // Shuffle the deck and origins.
            ShufflePiles(deck);
            if (origins != null) ShufflePiles(origins);
        }

        private void ShufflePiles<T>(T[] data)
        {
            // Split the data into piles randomly but uniformly.
            var piles = new List<T>[_piles];
            for (int i = 0; i < _piles; i++) piles[i] = new List<T>();

            foreach (T item in data.OrderBy(x => _random.Next()))
            {
                int randomPileIndex = _random.Next(_piles);
                piles[randomPileIndex].Add(item);
            }

            // Randomly decide the order to put the piles back together.
            var pileOrder = Enumerable.Range(0, _piles).OrderBy(x => _random.Next()).ToArray();

            // Put the piles back together in the decided order.
            int index = 0;
            foreach (int pileIndex in pileOrder)
            {
                foreach (T item in piles[pileIndex])
                {
                    data[index] = item;
                    index++;
                }
            }
        }
    }
}
