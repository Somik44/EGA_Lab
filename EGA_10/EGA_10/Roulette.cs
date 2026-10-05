using System;
using System.Collections.Generic;
using System.Linq;

namespace EGA_10
{
    internal class Roulette
    {
        private static readonly Random rnd = new Random();

        public static int TspRoulette(List<int> way, List<List<double>> matrix)
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

        public static (Individ parent1, Individ parent2) EgaRoulette(List<Individ> population)
        {
            Individ parent1 = RouletteSelect(population);

            var rest = population.Where(x => !ReferenceEquals(x, parent1)).ToList();

            Individ parent2 = RouletteSelect(rest);

            return (parent1, parent2);
        }

        private static Individ RouletteSelect(List<Individ> pop)
        {
            double sum = pop.Sum(x => x.Fitness);
            double value = rnd.NextDouble() * sum;

            double acc = 0;
            foreach (var ind in pop)
            {
                acc += ind.Fitness;
                if (value < acc) return ind;
            }
            return pop[^1];
        }
    }
}