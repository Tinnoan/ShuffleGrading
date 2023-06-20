using ShuffleGrading.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ShuffleGrading.Training
{
    public class CSVWriter : IDataWriter
    {
        public void Write(IOutputWriter writer, string? shuffleMethod, int[] shuffledDeck, int[] originalDeck, int numOfShuffles, int deckSize,
            object[]? parameters)
        {
            if (shuffledDeck.Length != originalDeck.Length)
            {
                throw new ArgumentException("Shuffled deck and original deck must be the same size");
            }

            writer.Append($"{shuffleMethod},");
            writer.AppendJoin(";", shuffledDeck);
            writer.Append(',');
            writer.AppendJoin(";", originalDeck);
            writer.Append(',');
            writer.Append(numOfShuffles);
            writer.Append(',');
            writer.Append(deckSize);
            if (parameters != null)
            {
                writer.Append(',');
                writer.AppendJoin(";", parameters);
            }
            writer.AppendLine();
        }

        public void WriteHeader(IOutputWriter writer, string header)
        {
            writer.AppendLine(header);
        }

        public void Save(IOutputWriter writer, string filename)
        {
            writer.Save(filename);
        }
    }
}
