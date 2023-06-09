﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuffleGrading.ShuffleTypes
{
    public class Ideal : IShuffle
    {
        /// <summary>
        /// This is an ideal shuffle. Using the Fisher-Yates algorithm, it shuffles the deck in O(n) time.
        /// </summary>
        public Ideal()
        {
            
        }
        private static readonly Random Rng = new();

        public string? Name { get; } = "Ideal shuffle";

        public void Shuffle(int[] deck, bool[]? origins)
        {
            int n = deck.Length;
            while (n > 1)
            {
                n--;
                int k = Rng.Next(n + 1);

                // Swap the cards.
                (deck[k], deck[n]) = (deck[n], deck[k]);

                if (origins == null) continue;
                // Swap the origins.
                (origins[k], origins[n]) = (origins[n], origins[k]);
            }
        }
    }
}
