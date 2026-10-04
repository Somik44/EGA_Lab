using System;
using System.Collections.Generic;
using System.Linq;

namespace EGA_9
{
    internal class TspRoulette
    {
        private static readonly Random rnd = new Random();

        public static int Roulette(List<int> way, List<List<double>> matrix)
        {
            int nowCity = way[^1];
            List<int> notInWay = new List<int>();
            for (int i = 0; i < matrix.Count; i++)
            {
                if (!way.Contains(i)) notInWay.Add(i);
            }

            double sum = 0;
            List<double> intervals = new List<double>();
            intervals.Add(0);
            foreach (int i in notInWay)
            {
                sum += 1 / matrix[nowCity][i];
                intervals.Add(sum);
            }

            double value = rnd.NextDouble() * sum;

            Console.WriteLine();
            for (int i = 0; i < notInWay.Count; i++)
            {
                double w = intervals[i + 1] - intervals[i];
                Console.WriteLine($"    Город {notInWay[i]}: P = {w / sum:P2}");
            }

            for (int i = 0; i < intervals.Count - 1; i++)
            {
                if (intervals[i] <= value && intervals[i + 1] > value)
                {
                    double p = (intervals[i + 1] - intervals[i]) / sum;
                    return notInWay[i];
                }
            }

            return -1;
        }

        public static (int From, int To) Roulette(List<(int From, int To)> pairs, List<List<double>> matrix)
        {
            double sum = 0;
            List<double> intervals = new List<double>();
            intervals.Add(0);
            foreach (var pair in pairs)
            {
                sum += 1 / matrix[pair.From][pair.To];
                intervals.Add(sum);
            }

            double value = rnd.NextDouble() * sum;

            Console.WriteLine();
            for (int i = 0; i < pairs.Count; i++)
            {
                double w = intervals[i + 1] - intervals[i];
                Console.WriteLine($"    Пара ({pairs[i].From} -> {pairs[i].To}): P = {w / sum:P2}");
            }

            for (int i = 0; i < intervals.Count - 1; i++)
            {
                if (intervals[i] <= value && intervals[i + 1] > value)
                {
                    double p = (intervals[i + 1] - intervals[i]) / sum;
                    return pairs[i];
                }
            }

            return (-1, -1);
        }
    }
}