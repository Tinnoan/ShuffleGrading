using System.Reflection;
using System.Runtime.InteropServices;
using ShuffleGrading.Grading;
using static ShuffleGrading.Program;

namespace ShuffleGrading
{
    class Program
    {
        private const int DeckSize = 10;
        private const int Iterations = 10000;
        private static readonly HashSet<Result> Results = new();

        static void Main(string[] args)
        {
            int[] deck = InitializeDeck();
            List<IGradingMetric> gradingMetrics = new List<IGradingMetric>
            {
                //new Entropy(5),
                //new RisingSequence(),
                //new InversionCount(),
                //new RiffleTest(),
                //new RunsTest(),
                new ChiSquaredTest()
            };
            //List<IGradingMetric> gradingMetrics = new List<IGradingMetric>
            //{
            //    new InversionCount()
            //};

            ShuffleGrading(ShuffleMethods.TopToBottomShuffle, 5, gradingMetrics);
            ShuffleGrading(ShuffleMethods.PerfectShuffle, 5, gradingMetrics);
            ShuffleGrading(ShuffleMethods.PerfectRiffleShuffle, 5, gradingMetrics);
            ShuffleGrading(ShuffleMethods.RiffleShuffle, 5, gradingMetrics);

            Console.ReadLine();
        }

        static int[] InitializeDeck()
        {
            int[] deck = new int[DeckSize];
            for (int i = 0; i < DeckSize; i++)
                deck[i] = i + 1;
            return deck;
        }

        static void ShuffleGrading(Action<int[], bool[]> shuffleMethod, int times, List<IGradingMetric> gradingMetrics)
        {
            int[] deck = InitializeDeck();

            foreach (var gradingMetric in gradingMetrics)
            {
                var result = new Result();
                List<double> scores = new List<double>();

                for (int i = 0; i < Iterations; i++)
                {
                    var origins = new bool[deck.Length];
                    for (int j = 0; j < origins.Length / 2; j++)
                    {
                        origins[j] = true; // or false, doesn't really matter as long as it's consistent
                    }
                    Shuffle(deck, origins, shuffleMethod, times);
                    scores.Add(gradingMetric.Grade(deck, origins));
                    ResetDeck(deck);
                    ResetOrigins(origins);
                    //PrintScore(shuffleMethod.Method.Name, gradingMetric.ToString(), scores);
                }

                result.Max = scores.Max();
                result.Min = scores.Min();
                result.Mean = scores.Average();
                result.Median = Median(scores);
                result.Name = shuffleMethod.Method.Name;
                result.GradingMetric = gradingMetric?.ToString();
                result.StandardDeviation = CalculateStandardDeviation(scores);
                result.Times = times;
                Results.Add(result);
                
                Console.WriteLine("Shuffle method: " + result.Name);
                Console.WriteLine("Grading method: " + result.GradingMetric);
                Console.WriteLine("Max score: " + result.Max);
                Console.WriteLine("Min score: " + result.Min);
                Console.WriteLine("Mean score: " + result.Mean);
                Console.WriteLine("Median score: " + result.Median); 
                Console.WriteLine("Standard deviation: " + result.StandardDeviation);
                Console.WriteLine("###");
            }
        }

        static void PrintScore(string shuffleMethod, string gradingMethod, List<double> scores)
        {
            Console.WriteLine("Shuffle method: " + shuffleMethod);
            Console.WriteLine("Grading method: " + gradingMethod);
            Console.WriteLine("Max score: " + scores.Max());
            Console.WriteLine("Min score: " + scores.Max());
            Console.WriteLine("Mean score: " + scores.Max());
            Console.WriteLine("Median score: " + scores.Max());
            Console.WriteLine("Standard deviation: " + scores.Max());
            Console.WriteLine("###");
        }

        // Print origins array
        static void PrintOrigins(bool[] origins)
        {
            Console.WriteLine(string.Join(", ", origins));
        }

        static void PrintDeck(int[] deck)
        {
            Console.WriteLine(string.Join(", ", deck));
        }

        static void ResetDeck(int[] deck)
        {
            for (int i = 0; i < DeckSize; i++)
                deck[i] = i + 1;
        }

        static void ResetOrigins(bool[] origins)
        {
            for (int i = 0; i < origins.Length / 2; i++)
            {
                origins[i] = true;
            }
            for (int i = origins.Length / 2; i < origins.Length; i++)
            {
                origins[i] = false;
            }
        }


        static void Shuffle(int[] deck, bool[] origins, Action<int[], bool[]> shuffleMethod, int times)
        {
            for (int i = 0; i < times; i++)
            {
                shuffleMethod(deck, origins);
                //PrintDeck(deck);
                //PrintOrigins(origins);
            }
        }


        static double Median(List<double> scores)
        {
            int numberCount = scores.Count();
            int halfIndex = numberCount / 2;
            var sortedNumbers = scores.OrderBy(n => n);
            double median;
            if ((numberCount % 2) == 0)
            {
                median = ((sortedNumbers.ElementAt(halfIndex) +
                           sortedNumbers.ElementAt((halfIndex - 1))) / 2);
            }
            else
            {
                median = sortedNumbers.ElementAt(halfIndex);
            }
            return median;
        }
        public static double CalculateStandardDeviation(List<double> values)
        {
            double mean = values.Average();
            double sumOfSquaresOfDifferences = values.Select(val => (val - mean) * (val - mean)).Sum();
            double standardDeviation = Math.Sqrt(sumOfSquaresOfDifferences / values.Count);
            return standardDeviation;
        }


        public class Result : IResult
        {
            public string Name { get; set; }
            public int Permutations { get; set; }
            public int Times { get; set; }
            public double Max { get; set; }
            public double Min { get; set; }
            public double Mean { get; set; }
            public double Median { get; set; }
            public string? GradingMetric { get; set; }
            public double StandardDeviation { get; set; }
        }
    }

    internal interface IResult
    {
        public string Name { get; set; }
        public int Permutations { get; set; }
        public int Times { get; set; }
        public double Max { get; set; }
        public double Min { get; set; }
        public double Mean { get; set; }
        public double Median { get; set; }
    }
}