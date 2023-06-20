using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ShuffleGrading.Training
{
    public interface IDataWriter
    {
        void Write(StringBuilder writer,string? shuffleMethod, int[] shuffledDeck, int[] originalDeck, int numOfShuffles, int deckSize, object[]? parameters);

        void WriteHeader(StringBuilder writer, string header);

        void Save(StringBuilder writer);
    }
}
