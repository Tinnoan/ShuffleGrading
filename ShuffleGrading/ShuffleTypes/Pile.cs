using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuffleGrading.ShuffleTypes
{
    public class Pile : IShuffle
    {
        private readonly int _piles;

        /// <summary>
        /// This is a pile shuffle. It takes the deck and splits it into piles, then puts the piles back together.
        /// </summary>
        /// <param name="piles">This indicates the number of piles the pile shuffle will use.</param>
        public Pile(int piles)
        {
            _piles = piles;
            Name = $"pile shuffle-{piles}";
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
            // Split the data into piles.
            var piles = new List<T>[_piles];
            for (int i = 0; i < _piles; i++) piles[i] = new List<T>();
            for (int i = 0; i < data.Length; i++) piles[i % _piles].Add(data[i]);

            // Put the piles back together.
            int index = 0;
            foreach (var pile in piles)
            {
                foreach (var item in pile)
                {
                    data[index] = item;
                    index++;
                }
            }
        }
    }

}
