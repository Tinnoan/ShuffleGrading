using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ShuffleGrading.Helpers;

namespace ShuffleGrading.Training
{
    public interface IDataWriter
    {
        void Write(IOutputWriter writer,string? shuffleMethod, int[] shuffledDeck, int[] originalDeck, int numOfShuffles, int deckSize, object[]? parameters);

        void WriteHeader(IOutputWriter writer, string header);

        void Save(IOutputWriter writer);
    }
}
