using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuffleGrading.Results
{
    public class Result : IResult
    {
        public string? Name { get; set; }
        public int Permutations { get; set; }
        public int Times { get; set; }
        public double Max { get; set; }
        public double Min { get; set; }
        public double Mean { get; set; }
        public double Median { get; set; }
        public string? GradingMetric { get; set; }
        public double StandardDeviation { get; set; }
        public List<double>? Scores { get; set; }
    }
}
