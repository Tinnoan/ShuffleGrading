using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuffleGrading.Helpers
{
    public class StreamOutputWriter : IOutputWriter
    {
        private readonly StreamWriter _streamWriter;

        public StreamOutputWriter(string filename)
        {
            _streamWriter = new StreamWriter(filename);
        }

        public void AppendLine()
        {
            _streamWriter.WriteLine();
        }

        public void AppendLine<T>(T? value)
        {
            _streamWriter.WriteLine(value);
        }

        public void Append<T>(T value)
        {
            _streamWriter.Write(value);
        }

        public void AppendJoin<T>(string separator, IEnumerable<T> values)
        {
            _streamWriter.Write(string.Join(separator, values));
        }

        public void Save(string filename)
        {
            // No need to save as the StreamWriter writes directly to the file
        }

        public void Dispose()
        {
            _streamWriter.Close();
        }
    }

}
