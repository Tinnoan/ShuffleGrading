using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuffleGrading.Helpers
{
    public interface IOutputWriter : IDisposable
    {
        void AppendLine();
        void AppendLine<T>(T? value);
        void Append<T>(T value);
        void AppendJoin<T>(string separator, IEnumerable<T> values);
        void Save(string filename);
    }
}
