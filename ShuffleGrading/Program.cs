using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using ShuffleGrading.Grading;
using ShuffleGrading.Results;
using ShuffleGrading.ShuffleTypes;
using ShuffleGrading.Training;
using static ShuffleGrading.Program;

namespace ShuffleGrading
{
    class Program
    {
        private const int DeckSize = 60;
        private const int Iterations = 1000;
        private static readonly HashSet<IResult> Results = new();
        private static Random _random = new();

        static void Main(string[] args)
        {
            int[] deck = InitializeDeck();
            List<IGradingMetric> gradingMetrics = new List<IGradingMetric>
            {
                new Entropy(5),
                new RisingSequence(),
                new InversionCount(),
                new RiffleTest(),
                new RunsTest(),
                new ChiSquaredTest(),
                new DistributionDistance(),
                new PermutationTest(),
                new SpearmanRankCorrelation(),
                new KendallsTau()
            };
            IShuffle[] shuffleTypes = {
                new TopToBottom(),
                new Ideal(),
                new PerfectRiffle(),
                new Riffle(),
                new Overhand()
            };

            //ShuffleGrading(new TopToBottom(), 5, gradingMetrics);
            //ShuffleGrading(new Ideal(), 5, gradingMetrics);
            //ShuffleGrading(new PerfectRiffle(), 5, gradingMetrics);
            //ShuffleGrading(new Riffle(), 5, gradingMetrics);
            //ShuffleGrading(new Overhand(), 5, gradingMetrics);
            ShuffleWriteData(shuffleTypes, 5, new CSVWriter());

            Console.ReadLine();
        }

        static int[] InitializeDeck(bool random = false)
        {
            int[] deck = new int[DeckSize];

            for (int i = 0; i < DeckSize; i++)
                deck[i] = i + 1;

            if (random)
            {
                Ideal idealShuffle = new();
                idealShuffle.Shuffle(deck, null);
            }

            return deck;
        }

        static void ShuffleGrading(IShuffle shuffleType, int times, IReadOnlyCollection<IGradingMetric> gradingMetrics)
        {
            int[] deck = InitializeDeck();
            int[] originalDeck = (int[])deck.Clone();

            foreach (var gradingMetric in gradingMetrics)
            {
                var result = new Result();
                List<double>? scores = new List<double>();

                for (int i = 0; i < Iterations; i++)
                {
                    var origins = new bool[deck.Length];
                    for (int j = 0; j < origins.Length / 2; j++)
                    {
                        origins[j] = true; // or false, doesn't really matter as long as it's consistent
                    }
                    Shuffle(deck, origins, shuffleType.Shuffle, times);
                    scores.Add(gradingMetric.Grade(deck, origins, originalDeck));
                    ResetDeck(deck);
                    ResetOrigins(origins);
                    //PrintScore(shuffleMethod.Method.Name, gradingMetric.ToString(), scores);
                }

                result.Max = scores.Max();
                result.Min = scores.Min();
                result.Mean = scores.Average();
                result.Median = Median(scores);
                result.Name = shuffleType.Name;
                result.GradingMetric = gradingMetric.Name;
                result.StandardDeviation = CalculateStandardDeviation(scores);
                result.Times = times;
                result.Scores = scores;
                Results.Add(result);
                
                PrintScore(result.Name, result.GradingMetric, result.Scores);
            }
        }

        static void ShuffleWriteData(IShuffle[] shuffleTypes, int times, IDataWriter dataWriter)
        {
            StringBuilder writer = new StringBuilder();
            int[] deck = InitializeDeck(true);
            int[] originalDeck = (int[])deck.Clone();

            for (int i = 0; i < Iterations; i++)
            {
                IShuffle shuffleType = shuffleTypes[_random.Next(0, shuffleTypes.Length)];
                var origins = new bool[deck.Length];
                for (int j = 0; j < origins.Length / 2; j++)
                {
                    origins[j] = true; // or false, doesn't really matter as long as it's consistent
                }

                Shuffle(deck, origins, shuffleType.Shuffle, times);
                dataWriter.WriteHeader(writer, "ShuffleMethod,ShuffledDeck,OriginalDeck,numOfShuffles,deckSize");  // header);
                dataWriter.Write(writer, shuffleType.Name, deck, originalDeck, times, deck.Length, null);

                ResetDeck(deck);
                ResetOrigins(origins);
            }

            dataWriter.Save(writer);
        }

        static void PrintScore(string? shuffleMethod, string? gradingMethod, IReadOnlyCollection<double>? scores)
        {
            Console.WriteLine($"Shuffle method: {shuffleMethod} | Grading Method: {gradingMethod}");
            Console.WriteLine($"Max score: {scores.Max()} | Min score: {scores.Min()} | Mean score: {scores.Average()} | Median score: {Median(scores)}");
            Console.WriteLine($"Standard deviation: {CalculateStandardDeviation(scores)}");
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


        static double Median(IReadOnlyCollection<double>? scores)
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
        public static double CalculateStandardDeviation(IReadOnlyCollection<double>? values)
        {
            double mean = values.Average();
            double sumOfSquaresOfDifferences = values.Select(val => (val - mean) * (val - mean)).Sum();
            double standardDeviation = Math.Sqrt(sumOfSquaresOfDifferences / values.Count);
            return standardDeviation;
        }
    }
}