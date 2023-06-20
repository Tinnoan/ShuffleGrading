using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuffleGrading.Helpers
{
    public class StringBuilderOutputWriter : IOutputWriter
    {
        private readonly StringBuilder _builder;

        public StringBuilderOutputWriter()
        {
            _builder = new StringBuilder();
        }

        public void AppendLine() => _builder.AppendLine();

        public void AppendLine<T>(T? value)
        {
            Debug.Assert(value != null, nameof(value) + " != null");
            _builder.AppendLine(value.ToString());
        }

        public void Append<T>(T value)
        {
            _builder.Append(value);
        }

        public void AppendJoin<T>(string separator, IEnumerable<T> values)
        {
            _builder.AppendJoin(separator, values);
        }

        public void Save(string filename)
        {
            File.WriteAllText(filename, _builder.ToString());
        }

        public void Dispose()
        {
            _builder.Clear();
        }
    }

}
