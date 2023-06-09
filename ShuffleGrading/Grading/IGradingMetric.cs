using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuffleGrading.Grading
{
    public interface IGradingMetric
    {
        double Grade(int[] deck, bool[] origins);
    }
}
