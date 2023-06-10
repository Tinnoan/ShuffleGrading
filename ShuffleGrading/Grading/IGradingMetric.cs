using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuffleGrading.Grading
{
    public interface IGradingMetric
    {
        string? Name { get; }
        double Grade(int[] deck, bool[] origins);
    }
}
