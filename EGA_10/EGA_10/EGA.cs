using System;
using System.Collections.Generic;
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
        public static void Algorithm(List<List<double>> matrix, int N, int populationCount)
        {
            var population = CreatePopulation(matrix, populationCount);
            int i = 0;

            while (i < N)
            {
                List<Individ> newPopulation = new List<Individ>();

                var top2 = population.OrderBy(i => i.Length).Take(2).ToList();
                newPopulation.Add(population[0]); newPopulation.Add(population[1]);
                for (int j = 0; j < population.Count - 2; j++)
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

            }
        }

        private static List<Individ> CreatePopulation(List<List<double>> matrix, int populationCount)
        {
            List<Individ> population = new List<Individ>();

            while (population.Count < populationCount)
            {
                if (population.Count < populationCount/2)
                {
                    var way = TspNearestNeighborSolver.Tsp(matrix);
                    double lenghtWay = LenghtWay(way, matrix);
                    var individ = new Individ(way, lenghtWay, 1/lenghtWay);
                    if (!population.Contains(individ)) population.Add(individ); 
                }
                else
                {
                    var way = Shuffle(matrix.Count);
                    double lenghtWay = LenghtWay(way, matrix);
                    var individ = new Individ(way, lenghtWay, 1 / lenghtWay);
                    if (!population.Contains(individ)) population.Add(individ);
                }
            }

            return population;
        }

        private static List<int> Shuffle(int n)
        {
            Random rng = new Random();
            List<int> way = Enumerable.Range(0, n).ToList();

            for (int i = n - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);
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

        }
    }
}
