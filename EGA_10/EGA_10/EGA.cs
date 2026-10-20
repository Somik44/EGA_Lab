using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGA_10
{
    public class Individ
    {
        public List<int> Way { get; set; }
        public double Length { get; set; }
        public double Fitness { get; set; }

        public Individ(List<int> way, double lenght, double fitness)
        {
            Way = way;
            Length = lenght;
            Fitness = fitness;
        }
    }

    internal class EGA
    {
        private static Random rnd = new Random();
        public static void Algorithm(List<List<double>> matrix, int N, int populationCount, int lenghtStagnation, double oddsMutation)
        {
            var population = CreatePopulation(matrix, populationCount);
            int i = 0;
            PrintGeneration(i, population, false);
            int stagnation = 0;
            Individ topIndivid;

            while (i < N && stagnation < lenghtStagnation)
            {
                List<Individ> newPopulation = new List<Individ>();

                var top2 = population.OrderBy(k => k.Length).Take(2).ToList();
                newPopulation.Add(new Individ(top2[0].Way.ToList(), top2[0].Length, top2[0].Fitness));
                newPopulation.Add(new Individ(top2[1].Way.ToList(), top2[1].Length, top2[1].Fitness));

                topIndivid = top2[0];

                for (int j = 2; j < populationCount * 2; j++)
                {
                    if (rnd.Next(0,2) == 0)
                    {
                        var pair = Roulette.EgaRoulette(population);
                        newPopulation.Add(OX(pair.parent1, pair.parent2, matrix));
                    }
                    else
                    {
                        var pair = Roulette.EgaRoulette(population);
                        newPopulation.Add(PMX(pair.parent1, pair.parent2, matrix));
                    }
                }

                for (int j = 2; j < newPopulation.Count; j++)
                {
                    if (rnd.NextDouble() < oddsMutation)
                    {
                        if (rnd.Next(0,2) == 0) SwapMutation(newPopulation[j], matrix);
                        else TwoOptMutation(newPopulation[j], matrix);
                    }
                }

                population.Clear();
                
                for (int j = 0; j < populationCount; j++)
                {
                    if (rnd.Next(0, 2) == 0) PropSelection(population, newPopulation);
                    else TourneySelection(population, newPopulation);
                }
                i++;

                PrintGeneration(i, population, false);

                if (topIndivid.Length == TopIndivid(population).Length) stagnation++;
                else stagnation = 0;
            }

            PrintGeneration(i, population, true);
        }

        private static List<Individ> CreatePopulation(List<List<double>> matrix, int populationCount)
        {
            List<Individ> population = new List<Individ>();
            var seen = new HashSet<string>();

            while (population.Count < populationCount)
            {
                List<int> way;

                if (population.Count < populationCount / 2)
                    way = TspNearestNeighborSolver.Tsp(matrix);
                else
                    way = Shuffle(matrix.Count);  

                string key = string.Join(",", way);

                if (!seen.Add(key)) continue;

                double length = LenghtWay(way, matrix);
                population.Add(new Individ(way, length, 1.0 / length));
            }

            return population;
        }

        private static List<int> Shuffle(int n)
        {
            List<int> way = Enumerable.Range(0, n).ToList();

            for (int i = n - 1; i > 0; i--)
            {
                int j = rnd.Next(i + 1);
                (way[i], way[j]) = (way[j], way[i]);
            }

            return way;
        }

        private static double LenghtWay(List<int> way, List<List<double>> matrix)
        {
            double sum = 0;
            for (int i = 0; i < way.Count; i++)
            {
                if (i == way.Count - 1) sum += matrix[way[i]][way[0]];
                else
                {
                    sum += matrix[way[i]][way[i + 1]];
                }
            }
            return sum;
        }

        private static Individ OX(Individ p1, Individ p2, List<List<double>> matrix)
        {
            int n = matrix.Count;
            List<int> child = Enumerable.Repeat(-1, n).ToList();
            
            int len = rnd.Next(1, n);
            int start = rnd.Next(0, n - len + 1);
            int end = start + len;

            for (int i = start; i< end; i++)
            {
                child[i] = p1.Way[i];
            }

            for (int i = end; i < n; i++)
            {
                for (int j = 0; j < p2.Way.Count; j++)
                {
                    if (!child.Contains(p2.Way[j]))
                    {
                        child[i] = p2.Way[j];
                        break;
                    }
                }
            }

            for (int i = 0; i < start; i++)
            {
                for (int j=0; j < p2.Way.Count; j++)
                {
                    if (!child.Contains(p2.Way[j]))
                    {
                        child[i] = p2.Way[j];
                        break;
                    }
                }
            }

            double lenght = LenghtWay(child, matrix);
            return new Individ(child, lenght, 1.0 / lenght);
        }

        private static Individ PMX (Individ p1, Individ p2, List<List<double>> matrix)
        {
            int n = matrix.Count;
            List<int> child = p1.Way.ToList();
            Dictionary<int, int> swap = new Dictionary<int, int>();

            int len = rnd.Next(1, n);
            int start = rnd.Next(0, n - len + 1);
            int end = start + len;

            for (int i = start; i < end; i++)
            {
                swap[p2.Way[i]] = child[i];
                child[i] = p2.Way[i];
            }

            for (int i=0; i< n; i++)
            {
                if (i >= start && i < end) continue;
                while (swap.ContainsKey(child[i]))
                {
                    child[i] = swap[child[i]];
                }
            }

            double lenght = LenghtWay(child, matrix);
            return new Individ(child, lenght, 1.0 / lenght);
        }

        private static void SwapMutation(Individ individ, List<List<double>> matrix)
        {
            int[] swap = new int[2];
            swap[0] = rnd.Next(0, individ.Way.Count);
            swap[1] = rnd.Next(0, individ.Way.Count);
            while (swap[0] == swap[1]) swap[1] = rnd.Next(0, individ.Way.Count);

            int temp = individ.Way[swap[0]];
            individ.Way[swap[0]] = individ.Way[swap[1]];
            individ.Way[swap[1]] = temp;


            individ.Length = LenghtWay(individ.Way, matrix);
            individ.Fitness = 1.0 / individ.Length;
        }

        private static void TwoOptMutation(Individ individ, List<List<double>> matrix)
        {
            List<int> way = individ.Way.ToList();

            int[] swap = new int[4];
            double old, now;

            swap[0] = rnd.Next(0, way.Count - 1);
            swap[1] = swap[0] + 1;

            swap[2] = rnd.Next(0, way.Count - 1);
            while (swap[2] == swap[0] || swap[2] == swap[1] + 1 || swap[2] + 1 == swap[0])
            {
                swap[2] = rnd.Next(0, way.Count - 1);
            }
            swap[3] = swap[2] + 1;

            if (swap[0] > swap[2])
            {
                (swap[0], swap[2]) = (swap[2], swap[0]);
                (swap[1], swap[3]) = (swap[3], swap[1]);
            }

            old = matrix[individ.Way[swap[0]]][individ.Way[swap[1]]] + matrix[individ.Way[swap[2]]][individ.Way[swap[3]]];

            for (int l = swap[1], r = swap[2]; l < r; l++, r--)
                (individ.Way[l], individ.Way[r]) = (individ.Way[r], individ.Way[l]);

            now = matrix[individ.Way[swap[0]]][individ.Way[swap[1]]] + matrix[individ.Way[swap[2]]][individ.Way[swap[3]]];


            individ.Length = LenghtWay(individ.Way, matrix);
            individ.Fitness = 1.0 / individ.Length;
        }

        private static void PropSelection(List<Individ> popylation, List<Individ> newPopulation)
        {
            popylation.Add(Roulette.RouletteSelect(newPopulation));
        }

        private static void TourneySelection(List<Individ> population, List<Individ> pool, int k = 3)
        {
            Individ winner = null;
            for (int i = 0; i < k; i++)
            {
                var candidate = pool[rnd.Next(pool.Count)]; 
                if (winner == null || candidate.Length < winner.Length)
                    winner = candidate;
            }
            population.Add(winner);
        }

        private static Individ TopIndivid(List<Individ> population) => population.OrderBy(x => x.Length).First();

        private static void PrintGeneration(int generation, List<Individ> population, bool end)
        {
            var best = TopIndivid(population);

            if (!end) Console.WriteLine($"\n========== Поколение {generation} ==========");
            else Console.WriteLine($"\nПоследнее поколение - {generation}");
            Console.WriteLine($"Лучшая особь:");
            Console.WriteLine($"  Кодировка:        [ {string.Join(", ", best.Way)} ]");
            Console.WriteLine($"  Длина цикла:      {best.Length:F2}");
            Console.WriteLine($"  Приспособленность: {best.Fitness:F6}");

            if (!end)
            {
                Console.WriteLine($"\nВсе особи в поколении ({population.Count}):");
                for (int j = 0; j < population.Count; j++)
                {
                    var ind = population[j];
                    Console.WriteLine(
                        $"  [{j,2}] " +
                        $"длина = {ind.Length,8:F2} | " +
                        $"fitness = {ind.Fitness:F6} | " +
                        $"путь = [ {string.Join(", ", ind.Way)} ]"
                    );
                }
            }

        }
    }
}
